<template ::key="deviceId">
  <div class="info__wrapper">
    <div class="info__toolbar d-flex">
      <!-- Xóa, cập nhật -->
      <v-dialog v-model="editPopup" persistent width="768">
        <template v-slot:activator="{ props }">
          <v-btn
            class="ma-1"
            elevation="4"
            theme="dark"
            color="#001664"
            prepend-icon="mdi-pencil-box"
            v-bind="props"
            >Edit</v-btn
          >
        </template>
        <v-card theme="light">
          <v-card-title>
            <span style="color: #000832; font-weight: bold" class="text-h5"
              >Device Information</span
            >
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    color="#000832"
                    label="Name *"
                    required
                    v-model="objectDevice.name"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-textarea
                    color="#000832"
                    label="Description"
                    rows="4"
                    v-model="objectDevice.description"
                  ></v-textarea>
                </v-col>
                <v-col cols="12" sm="6">
                  <v-select
                    color="#000832"
                    :items="['Active', 'Unactive']"
                    label="Status"
                    v-model="objectDevice.isActive"
                  ></v-select>
                </v-col>
                <v-col cols="12" sm="6">
                  <small
                    style="height: 80%"
                    class="d-flex align-center justify-center text-center w-100"
                    ><b style="color: red; margin-right: 4px">*</b> Indicates
                    required field</small
                  >
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="#000832" variant="text" @click="editPopup = false">
              Close
            </v-btn>
            <v-btn
              color="white"
              style="background-color: #001664"
              variant="text"
              @click="onClickSave"
            >
              Update
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-btn
        class="ma-1"
        elevation="4"
        theme="dark"
        color="#AA1616"
        prepend-icon="mdi-delete"
        v-bind="props"
        @click="onClickDeleteDevice"
        >Delete</v-btn
      >
    </div>

    <table class="info__grid">
      <tr>
        <td class="info__label">Name:</td>
        <td>
          <b>{{ device.name }}</b>
        </td>
      </tr>
      <tr>
        <td class="info__label">Description:</td>
        <td>
          <b>{{ device.description }}</b>
        </td>
      </tr>
      <tr>
        <td class="info__label">Status:</td>
        <td>
          <span
            style="
              height: 28px;
              width: 48px;
              border-radius: 14px;
              display: flex;
              align-items: center;
              background-color: #000832;
            "
          >
            <v-switch
              v-model="device.isActive"
              color="green"
              hide-details
              inset
              @click.stop
              @change="onChangeActiveDevice(deviceId, device.isActive, device.name)"
            >
            </v-switch>
            <b style="margin-left: 56px">{{ device.status }}</b>
          </span>
        </td>
      </tr>
      <tr>
        <td class="info__label">Last Active:</td>
        <td>
          <b>{{ device.activeTime }}</b>
        </td>
      </tr>
      <tr>
        <td class="info__label">Created At:</td>
        <td>
          <b>{{ device.createdTime }}</b>
        </td>
      </tr>
    </table>

    <!-- API Info -->
    <v-card class="mt-4" elevation="8">
      <div style="padding: 8px 16px 4px 16px">
        <b>Device API information for sending EEG data to Brainwave System:</b>
      </div>
      <table class="info__grid">
        <tr>
          <td class="info__label">URL:</td>
          <td style="padding: 0px">
            <pre><v-code>{{ apiInfo.baseURL + apiInfo.postDeviceEEGDataEndpoint }}</v-code></pre>
          </td>
        </tr>
        <tr>
          <td class="info__label">HTTP Method:</td>
          <td style="padding: 0px">
            <pre><v-code>{{ apiInfo.httpMethods.POST }}</v-code></pre>
          </td>
        </tr>
        <tr>
          <td class="info__label">Header:</td>
          <td style="padding: 0px">
            <pre><v-code>{{ apiInfo.header }}</v-code></pre>
          </td>
        </tr>
        <tr>
          <td class="info__label">Body (Payload) format:</td>
          <td style="padding: 0px">
            <pre><v-code>{{ apiInfo.bodyFormat }}</v-code></pre>
          </td>
        </tr>
      </table>
      <li style="margin: 2px 4px 0px 4px"><b>Sample CURL:</b></li>
      <pre><v-code language="javascript" theme="vs-dark">{{ apiInfo.postDeviceEEGDataCurl}}</v-code></pre>
      <!-- <pre><code>
          <span class="keyword">function</span> <span class="function">greeting</span>(<span class="parameter">name</span>) {
          <span class="keyword">return</span> <span class="string">'Hello, '</span> + name + <span class="string">'!'</span>;
          }

          <span class="keyword">var</span> <span class="variable">person</span> = <span class="string">'John'</span>;
          <span class="keyword">var</span> <span class="variable">message</span> = greeting(person);

          console.log(message);
        </code></pre> -->
    </v-card>

    <!-- Popup Confirm -->
    <v-dialog v-model="confirmDialog" persistent width="auto">
      <!-- <template v-slot:activator="{ props }">
        <v-btn color="primary" v-bind="props"> Open Dialog </v-btn>
      </template> -->
      <v-card>
        <v-card-title class="text-h5">
          {{ confirmInfo.title }}
        </v-card-title>
        <v-card-text>
          {{ confirmInfo.message }}
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn variant="text" @click="onCancelConfirm()"> Cancel </v-btn>
          <v-btn
            theme="dark"
            color="white"
            variant="text"
            @click="onDoConfirm()"
            style="background-color: #001664"
          >
            Ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Card (sheet) thông báo kết quả -->
    <v-dialog v-model="resultCard">
      <v-sheet
        elevation="12"
        max-width="400"
        rounded="lg"
        width="100%"
        class="pa-4 text-center mx-auto"
      >
        <div v-if="resultInfo.status == 0">
          <v-icon
            class="mb-5"
            color="error"
            icon="mdi-alert-circle"
            size="112"
          ></v-icon>
          <h2 class="text-h5 mb-6">Fail!</h2>
        </div>
        <div v-else-if="resultInfo.status == 1">
          <v-icon
            class="mb-5"
            color="success"
            icon="mdi-check-circle"
            size="112"
          ></v-icon>
          <h2 class="text-h5 mb-6">Success!</h2>
        </div>

        {{ resultInfo.message }}
        <br />
        <p
          class="mb-4 text-medium-emphasis text-body-2"
          v-if="resultInfo.linkHref"
        >
          {{ resultInfo.linkPreNote }}
          <router-link
            :to="resultInfo.linkHref"
            class="text-decoration-none text-info"
            >{{ resultInfo.linkTitle ? resultInfo.linkTitle : "Go to link." }}.
          </router-link>
        </p>
      </v-sheet>
    </v-dialog>

    <VtfLoading v-if="isShowLoading" />
  </div>
</template>

<script>
import axios from "axios";
import moment from "moment";
import VtfLoading from "@/components/VtfLoading.vue";

export default {
  components: {
    VtfLoading,
  },

  data() {
    return {
      device: {},
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      token: localStorage.getItem("token"),
      apiInfo: {
        getDeviceInfoEndpoint: `Devices/${this.deviceId}`,
        postDeviceEEGDataEndpoint: `DeviceDatas/${this.deviceId}`,
        httpMethods: { GET: "GET", POST: "POST", PUT: "PUT", DEL: "DELETE" },
        header: `{
  accept: text/plain,
  Content-Type: application/json
}`,
        bodyFormat: `{
  "general": {
    "poorQuality": <<Fill a interger value form 0 to 255>>,
    "attention": <<Fill a interger value form 0 to 255>>,
    "meditation": <<Fill a interger value form 0 to 255>>,
    "deviceId": "${this.deviceId}"
  },
  "delta": <<Fill a interger value form 0 to 255>>,
  "theta": <<Fill a interger value form 0 to 255>>,
  "alpha": <<Fill a interger value form 0 to 255>>,
  "lowBeta": <<Fill a interger value form 0 to 255>>,
  "midBeta": <<Fill a interger value form 0 to 255>>,
  "highBeta": <<Fill a interger value form 0 to 255>>,
  "gamma": <<Fill a interger value form 0 to 255>>,
  "uhfGamma": <<Fill a interger value form 0 to 255, preferably 512 number separated values (by ,)>>,
  "rawEEGs": [
    <<Fill some interger value form 0 to 255, >>
  ]
}`,
        postDeviceEEGDataCurl: `curl -X 'POST' \
  '${this.apiBaseURL}DeviceDatas/${this.deviceId}' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "general": {
    "poorQuality": 0,
    "attention": 0,
    "meditation": 0,
    "deviceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "delta": 0,
  "theta": 0,
  "alpha": 0,
  "lowBeta": 0,
  "midBeta": 0,
  "highBeta": 0,
  "gamma": 0,
  "uhfGamma": 0,
  "rawEEGs": [
    0
  ]
}'`,
      },

      code: `
        function greeting(name) {
          return 'Hello, ' + name + '!';
        }

        var person = 'John';
        var message = greeting(person);

        console.log(message);
      `,

      editPopup: false,
      confirmDialog: false,
      objectDevice: {
        name: undefined,
        description: "",
        isActive: "Active",
      },
      confirmInfo: {
        title: "",
        message: "",
      },
      confirmAction: 0, // 0: Unactive, 1: Delete, 2: Create, 3: Edit
      resultCard: false,
      resultInfo: {
        status: null, // 0: Fail, 1: Success, 2: Warning, 3: Note
        message: "",
        linkPreNote: "",
        linkHref: "",
        linkTitle: "",
      },
      isShowLoading: false,
    };
  },
  props: ["deviceId"],

  methods: {
    async getDeviceDetail() {
      this.isShowLoading = true;
      await axios
        .get(this.apiBaseURL + this.apiInfo.getDeviceInfoEndpoint, {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        })
        .then((res) => {
          this.device = res.data;
          if (res.data.isActive) {
            this.device.status = "Active";
          } else {
            this.device.status = "Unactive";
          }
          this.device.createdTime = moment(this.device.createdTime);
          this.device.activeTime = moment(this.device.activeTime);
          this.device.createdTime = this.device.createdTime.format(
            "hh:mm A - MMM DD, YYYY"
          );
          this.device.activeTime = this.device.activeTime.format(
            "hh:mm A - MMM DD, YYYY"
          );
        })
        .catch((err) => {
          console.log(err);
        });
      this.isShowLoading = false;
    },

    omClickSave() {
      this.confirmInfo = {
        title: "Update Device",
        message:
          "Are you sure you want to save the change infomation of this device?",
      };
      this.confirmDialog = true;
      this.confirmAction = 3;
    },

    // async updateDevice() {
    //   this.isShowLoading = true;
    //   try {
    //     if (this.objectDevice.isActive == "Unactive")
    //       this.objectDevice.isActive = false;
    //     else this.objectDevice.isActive = true;
    //     await axios
    //       .put(this.apiInfo.baseURL + this.apiInfo.getDeviceInfoEndpoint, this.objectDevice)
    //       .then((response) => {
    //         this.editPopup = false;
    //         this.resultInfo = {
    //           status: 1, // 0: Fail, 1: Success, 2: Warning, 3: Note
    //           message: "Updated device successfully.",
    //           linkPreNote: "",
    //           linkHref: "",
    //           linkTitle: "",
    //         };
    //         this.resultCard = true;
    //         this.$emit("onReloadListDevice");
    //         this.resetAfterConfirm();
    //         setTimeout(() => {
    //           this.resultCard = false;
    //         }, 6000);
    //       })
    //       .catch((err) => {
    //         console.log(err);
    //         this.isShowLoading = false;
    //         if (err.response.status != 400) this.editPopup = false;
    //         this.resultInfo = {
    //           status: 0, // 0: Fail, 1: Success, 2: Warning, 3: Note
    //           message: "Update device failed.",
    //           linkPreNote: "",
    //           linkHref: "",
    //           linkTitle: "",
    //         };
    //         this.resultCard = true;
    //         setTimeout(() => {
    //           this.resultCard = false;
    //         }, 6000);
    //       });
    //   } catch (ex) {
    //     console.log(ex);
    //   }
    //   // this.isShowLoading = false;
    // },

    onChangeActiveDevice(id, isActive, name) {
      if (isActive == false) {
        this.confirmInfo = {
          title: "Unactive Device",
          message: "Are you sure you want to unactive device " + name + "?",
        };
        this.confirmDialog = true;
        this.confirmAction = 0;
        this.selectedDeviceId = id;
      } else {
        this.callToggleActiveDevice(id);
      }
    },

    onClickDeleteDevice() {	
      this.confirmInfo = {	
        title: "Delete Device",	
        message: "Are you sure you want to delete device " + this.device.name + "?",	
      };	
      this.confirmDialog = true;	
      this.confirmAction = 1;	
      this.selectedDeviceId = this.device.id;	
    },

    onCancelConfirm() {
      //Nếu là unactive thì hủy action unactive đó
      if (this.confirmAction == 0)
        this.device.isActive = true;
      // Đóng Popup và reset
      this.confirmAction = 0;
      this.confirmDialog = false;
      this.confirmInfo = { title: "", message: "" };
      this.selectedDeviceId = null;
    },

    onDoConfirm() {
      if (this.confirmAction == 0)
        this.callToggleActiveDevice(this.selectedDeviceId);
      else if (this.confirmAction == 1)
        this.deletedDevice(this.selectedDeviceId);
      else if (this.confirmAction == 2) this.createDevice();
      this.resetAfterConfirm();
    },

    async callToggleActiveDevice(id) {
      axios
        .patch(this.apiBaseURL + "Devices/" + id + "/active", {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        })
        .then((response) => {
          console.log(response);
        })
        .catch((response) => {
          console.log(response);
        });
      // this.$emit("onRefresh");
      // const newList = await this.getListDevice(); // Cân nhắc bỏ nếu respone trên thành công vì performance
      // this.ListDevice = newList;
    },

    async deletedDevice(id) {
      this.isShowLoading = true;
      try {
        await axios
          .delete(this.apiBaseURL + "Devices/" + id, {
            headers: {
              Authorization: "Bearer " + this.token,
            },
          })
          .then(() => {
            this.resultInfo = {
              status: 1, // 0: Fail, 1: Success, 2: Warning, 3: Note
              message: "Xóa thiết bị thành công.",
            };
            this.resultCard = true;
            setTimeout(() => {
              this.resultCard = false;
            }, 6000);
            this.$emit("onReloadListDevice");
          })
          .catch((response) => {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      // this.isShowLoading = false;
    },

    resetAfterConfirm() {
      this.confirmAction = 0;
      this.confirmDialog = false;
      this.confirmInfo = { title: "", message: "" };
      this.selectedDeviceId = null;
      this.objectDevice = {
        name: undefined,
        description: "",
        isActive: "Active",
      };
    },
  },

  async created() {
    this.getDeviceDetail();
  },

  watch: {
    device(obj) {
      this.$emit("onSetAppName", obj.name);
    },
  },
};
</script>

<style scoped>
.info__wrapper {
  margin: 0px 4px;
  width: calc(100% - 8px);
  box-sizing: border-box;
}

.info__toolbar {
  justify-content: right;
}

.info__grid {
  width: 100%;
}
.info__grid td {
  padding: 2px 8px;
  border: 1px solid #001664;
  font-weight: bold;
}
.info__grid .info__label {
  min-width: 150px;
  border: none;
  background-color: #000832;
  color: white;
}

pre {
  /* background-color: black #f4f4f4; */
  color: white;
  border: 1px solid #ddd;
  /* padding: 10px; */
  border-radius: 4px;
  /* overflow: auto; */
  width: 100%;
  box-sizing: border-box;
  overflow-x: auto;
}

code {
  font-family: Consolas;
  font-size: 14px;
  width: 100%;
  font-weight: bold;
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