<template>
  <div class="info__wrapper">
    <div class="info__item">
      <span>Name:</span>
      <span>
        <b>{{ device.name }}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Description:</span>
      <span>
        <b>{{ device.description }}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Status:</span>
      <span>
        <b>{{ device.status }}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Created Date:</span>
      <span>
        <b>{{ device.createdTime }}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Send EEG Data API for Device:</span>
      <div>
        <li>URL:</li>
        <v-code>{{
          apiInfo.baseURL + apiInfo.postDeviceEEGDataEndpoint
        }}</v-code>
        <li>HTTP Method:</li>
        <v-code>{{ apiInfo.httpMethods.POST }}</v-code>
        <li>Header:</li>
        <v-code>{{ apiInfo.header }}</v-code>
        <li>Body format:</li>
        <v-code>{{ apiInfo.bodyFormat }}</v-code>
        <li>Sample CURL:</li>
        <v-code>{{ apiInfo.postDeviceEEGDataCurl }}</v-code>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      device: {},
      apiInfo: {
        baseURL: "https://localhost:44321/api/",
        getDeviceInfoEndpoint: "Devices/5b85104d-a8b4-4059-a3a0-f9991287b380",
        postDeviceEEGDataEndpoint:
          "DeviceDatas/5b85104d-a8b4-4059-a3a0-f9991287b380",
        httpMethods: { GET: "GET", POST: "POST", PUT: "PUT", DEL: "DELETE" },
        header:
          "```Javascript \n{\n    accept: text/plain,\n    Content-Type: application/json    \n}```",
        bodyFormat:
          "{\n" +
          '"general": {' +
          '"poorQuality": 0,' +
          '"attention": 0,' +
          '"meditation": 0,' +
          '"deviceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"' +
          "},\n" +
          '"delta": 0, ' +
          '"theta": 0, ' +
          '"alpha": 0,' +
          '"lowBeta": 0,' +
          '"midBeta": 0,' +
          '"highBeta": 0,' +
          '"gamma": 0,' +
          '"uhfGamma": 0,\n' +
          '"rawEEGs": [' +
          "  0 " +
          "]\n" +
          "}",
        postDeviceEEGDataCurl:
          "curl -X 'POST' \\" +
          "'https://localhost:44321/api/DeviceDatas/5b85104d-a8b4-4059-a3a0-f9991287b380' \\" +
          "-H 'accept: text/plain' \\" +
          "-H 'Content-Type: application/json' \\" +
          "-d '{" +
          '"general": {' +
          '  "poorQuality": 0,' +
          '  "attention": 0,' +
          '  "meditation": 0,' +
          '  "deviceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"' +
          "}," +
          '"delta": 0,' +
          '"theta": 0,' +
          '"alpha": 0,' +
          '"lowBeta": 0,' +
          '"midBeta": 0,' +
          '"highBeta": 0,' +
          '"gamma": 0,' +
          '"uhfGamma": 0,' +
          '  "rawEEGs": [' +
          "   0" +
          "  ]" +
          "}'\"",
      },
    };
  },

  created() {
    axios
      .get(this.baseURL + this.getDeviceInfoEndpoint)
      .then((res) => {
        this.device = res.data;
        console.log(res);
        if (res.data.isActive) {
          this.device.status = "Active";
        } else {
          this.device.status = "Unactive";
        }
      })
      .catch((err) => {
        console.log(err);
      });
  },
};
</script>

<style scoped>
.info__wrapper {
  margin: 32px;
}

.info__item {
  padding-bottom: 8px;
  display: grid;
  grid-template-columns: 128px auto;
}

.info__item span {
}
</style>