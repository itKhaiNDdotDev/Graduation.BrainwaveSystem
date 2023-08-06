#include <SoftwareSerial.h>
SoftwareSerial BT(10, 11); //RX | TX


// checksum variables
byte generatedChecksum = 0;
byte checksum = 0;
int payloadLength = 0;
byte payloadData[64] = {0};
byte poorQuality = 0;
byte attention   = 0;
byte meditation  = 0;
int rawData   = 0;

// system variables
long lastReceivedPacket = 0;
boolean bigPacket = false;
char mode = 0;
long f = 10;
unsigned long time;

// brainwave variables
byte eegvalues[8];

byte ReadOneByte() {
  int ByteRead;

  while (!Serial.available());
  ByteRead = Serial.read();

#if DEBUGOUTPUT
  Serial.print((char)ByteRead);   // echo the same byte out the USB serial (for debug purposes)
#endif

  return ByteRead;
}

void setup() {
  
  Serial.begin(57600);           
  BT.begin(57600);
}

void loop() {


  // Look for sync bytes
  if(ReadOneByte() == 170) {
    if(ReadOneByte() == 170) {

      payloadLength = ReadOneByte();
      if(payloadLength > 169)                      //Payload length can not be greater than 169
          return;

      generatedChecksum = 0;        
      for(int i = 0; i < payloadLength; i++) {  
        payloadData[i] = ReadOneByte();            //Read payload into memory
        generatedChecksum += payloadData[i];
      }   

      checksum = ReadOneByte();                      //Read checksum byte from stream      
      generatedChecksum = 255 - generatedChecksum;   //Take one's compliment of generated checksum

        if(checksum == generatedChecksum) {    

        poorQuality = 200;
        attention = 0;
        meditation = 0;

         for(int i = 0; i < payloadLength; i++) {    // Phân tích dữ liệu
          switch (payloadData[i]) {
          case 2: //poorQuality
            i++;            
            poorQuality = payloadData[i];
            bigPacket = true;            
            break;
          case 4: //attention
            i++;
            attention = payloadData[i];                        
            break;
          case 5: //meditation
            i++;
            meditation = payloadData[i];
            break;
          case 0x80: // rawData
            i++;
            rawData = (((int) payloadData[i+1] << 8) | payloadData[i+2]);
            //rawData = rawData*1.8/4096/2000;
            //Serial.println();
            //Serial.print(rawData, DEC);
//            BT.println(rawData, DEC);
            i+=2;
            break;
          case 0x83: // phân tích 8 cột sóng não
            i++;
            for (int k=i+1; k<=i+25; k+=3)
                eegvalues[(k-(i+1))/3+1]=(((unsigned long) payloadData[k] << 16) | (((unsigned long) payloadData[k+1] << 8) | ((unsigned long) payloadData[k+2]))); // biến đổi endian
            i+=24;
            break;
          default:
            break;
          } // switch
        } // for loop

#if !DEBUGOUTPUT

        // *** Add your code here ***

        if(bigPacket) {
//          BT.print(poorQuality, DEC); BT.print(" ");
//          BT.print(attention, DEC); BT.print(" ");
//          BT.print(meditation, DEC); BT.print(" ");
//          for (int k=1; k<=8; k++) {
//            BT.print(eegvalues[k], DEC); BT.print(" ");
//          }
//          Serial.print(poorQuality, DEC); Serial.print(" ");
          Serial.print(attention, DEC);   Serial.print(" ");
          Serial.print(meditation, DEC);   Serial.print(" ");
//          for (int k=1; k<=8; k++) {
//            Serial.print(eegvalues[k], DEC); Serial.print(" ");
//          }
//          if (BT.available() > 0) {
//            mode = BT.read();
//            Serial.print(mode);
//            BT.print(mode);
//          }
          Serial.print("\n");
//          BT.print("\n");
        }
#endif        
        bigPacket = false;        
      }
      else {
        // Checksum Error
      }  // end if else for checksum
    } // end if read 0xAA byte
  } // end if read 0xAA byte
}
