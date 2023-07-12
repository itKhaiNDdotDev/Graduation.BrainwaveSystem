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
        <br />
        <pre><code>
          <span class="keyword">function</span> <span class="function">greeting</span>(<span class="parameter">name</span>) {
          <span class="keyword">return</span> <span class="string">'Hello, '</span> + name + <span class="string">'!'</span>;
          }

          <span class="keyword">var</span> <span class="variable">person</span> = <span class="string">'John'</span>;
          <span class="keyword">var</span> <span class="variable">message</span> = greeting(person);

          console.log(message);
  </code></pre>

    <div>
    <v-code language="javascript" dark>
      function greeting(name) {
        return 'Hello, ' + name + '!';
      }

      var person = 'John';
      var message = greeting(person);

      console.log(message);
    </v-code>
  </div>
<br>
aaaa
  <v-code v-model="code" language="javascript" theme="vs-dark" />
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

      code: `
        function greeting(name) {
          return 'Hello, ' + name + '!';
        }

        var person = 'John';
        var message = greeting(person);

        console.log(message);
      `
    };
  },

  created() {
    axios
      .get(this.apiInfo.baseURL + this.apiInfo.getDeviceInfoEndpoint)
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

pre {
  background-color: black /*#f4f4f4*/;
  color: white;
  border: 1px solid #ddd;
  padding: 10px;
  overflow: auto;
}

code {
  font-family: Consolas, Monaco, Courier, monospace;
  font-size: 14px;
}

.function {
      color: #6f42c1;
    }

    .string {
      color: #032f62;
    }

    .comment {
      color: #6a737d;
      font-style: italic;
    }

    pre {
      background-color: #1e1e1e;
      color: #d4d4d4;
      padding: 10px;
      border-radius: 5px;
      overflow: auto;
    }

    .keyword {
      color: #d73a49;
      font-weight: bold;
    }

    .function {
      color: #c586c0;
    }

    .string {
      color: #ce9178;
    }

    .comment {
      color: #6a9955;
      font-style: italic;
    }

    pre {
      background-color: #1e1e1e;
      color: #d4d4d4;
      padding: 10px;
      border-radius: 5px;
      overflow: auto;
    }

    .keyword {
      color: #d73a49;
      font-weight: bold;
    }

    .function {
      color: #c586c0;
    }

    .string {
      color: #ce9178;
    }

    .comment {
      color: #6a9955;
      font-style: italic;
    }

    body {
      background-color: #1e1e1e;
      color: #d4d4d4;
    }
</style>