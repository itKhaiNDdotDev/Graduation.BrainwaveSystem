#include <ESP8266WiFi.h>
#include <ArduinoJson.h>
#include <ESP8266HTTPClient.h>

byte generatedChecksum = 0, checksum = 0;
int16_t payloadLength = 0, rawData = 0;
byte payloadData[64] = {0}, poorQuality = 0, attention = 0, meditation = 0, eegvalues[8];
long lastReceivedPacket = 0, f = 1;
boolean bigPacket = false;
int16_t sliding[5000],k=0;
const char* ssid = "Hust_TVTQB_Dien-Dien-tu";
const char* password = "";
const char* apiURL = "https://brwsyswebapis.azurewebsites.net/api/DeviceDatas/46414611-3d83-457c-9755-e585d56264aa";

byte ReadOneByte() {
  while (!Serial.available());
  int ByteRead = Serial.read();
  return ByteRead;
}

void setup() {
  Serial.begin(57600);
  connectWiFi(ssid, password);
}

void loop() {
  // read incoming data
  DynamicJsonDocument jsonDocument(2048);
  if (ReadOneByte() == 170 && ReadOneByte() == 170) {
    payloadLength = ReadOneByte();
    if (payloadLength > 169) return;
    generatedChecksum = 0;
    for (int i = 0; i < payloadLength; i++) {
      payloadData[i] = ReadOneByte();
      generatedChecksum += payloadData[i];
    }
    checksum = ReadOneByte();
    generatedChecksum = 255 - generatedChecksum;
    if (checksum == generatedChecksum) {
      poorQuality = 200;
      attention = 0;
      meditation = 0;
      for (int i = 0; i < payloadLength; i++) {
        switch (payloadData[i]) {
          case 2:
            i++;
            poorQuality = payloadData[i];
            bigPacket = true;
            break;
          case 4:
            i++;
            attention = payloadData[i];
            break;
          case 5:
            i++;
            meditation = payloadData[i];
            break;
          case 0x80:
            i++;
            rawData = (((int)payloadData[i + 1] << 8) | payloadData[i + 2]);
            //Serial.println();
            //Serial.print(rawData, DEC);
            sliding[k++] = rawData;
            jsonDocument["rawEEGs"].add(rawData);
            i+=2;
            break;
          case 0x83:
            i++;
            for (int k = i+1; k <= i+25; k+=3) {
              eegvalues[(k - (i + 1)) / 3 + 1] = (((unsigned long)payloadData[k] << 16) | (((unsigned long)payloadData[k + 1] << 8) | ((unsigned long)payloadData[k + 2])));  // biến đổi endian
              i+=2;
            }
            break;
          default:
            break;
        }
      }
      if (bigPacket) {
          jsonDocument["general"]["poorQuality"] = poorQuality;
          jsonDocument["general"]["attention"] = attention;
          jsonDocument["general"]["meditation"] = meditation;
          jsonDocument["general"]["deviceId"] = "46414611-3d83-457c-9755-e585d56264aa";
          jsonDocument["delta"] = eegvalues[1];
          jsonDocument["theta"] = eegvalues[2];
          jsonDocument["alpha"] = eegvalues[3];
          jsonDocument["lowBeta"] = eegvalues[4];
          jsonDocument["midBeta"] = eegvalues[5];
          jsonDocument["highBeta"] = eegvalues[6];
          jsonDocument["gamma"] = eegvalues[7];
          jsonDocument["uhfGamma"] = eegvalues[8];
          for (int it = 0; it < k; it++)
            jsonDocument["rawEEGs"][it] = sliding[it];
          sendDataToServer(apiURL, jsonDocument);
          k = 0;
      }
      bigPacket = false;
    } else {
      // Checksum Error
    }
  }
}

void connectWiFi(const char* ssid, const char* password) {
  WiFi.begin(ssid, password);
  Serial.print("Connecting to WiFi");
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println();
  Serial.println("Connected to WiFi");
}

void sendDataToServer(const char* apiURL, DynamicJsonDocument& jsonDocument) {
  // Serialize JSON object to string
  String jsonString;
  serializeJson(jsonDocument, jsonString);
  Serial.println(jsonString);
  // Send HTTP POST request with JSON payload
  WiFiClientSecure client;
  HTTPClient http;
  client.setInsecure(); // Disable SSL certificate validation
  http.begin(client, apiURL);
  http.addHeader("Content-Type", "application/json");
  int httpResponseCode = http.POST(jsonString);
  if (httpResponseCode > 0) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    String response = http.getString();
    Serial.println("Server response: " + response);
  } else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  http.end();
}