<template>
  <v-sheet class="pa-1 d-flex align-content-start flex-wrap">
    <!-- Danh sách -->
    <v-card v-for="item in ListDevice" :key="item.id" class="btn ma-4 d-flex justify-center align-center flex-wraơ"
      elevation="10" height="180" width="180" theme="dark" color="#000832" @click="onClickDevice(item.id)" nav>
      <div class="item__toolbar">
        <v-btn style="height: 28px; width: 48px; border-radius: 14px; color: #dd6448" icon="mdi-delete" color="#FF000066"
          @click.stop="onClickDeleteDevice(item.id, item.name)"></v-btn>
        <div style="height: 28px; display: flex; align-items: center">
          <v-switch v-model="item.isActive" color="green" hide-details inset @click.stop
            @change="onChangeActiveDevice(item.id, item.isActive, item.name)">
          </v-switch>
        </div>
      </div>
      <v-icon class="ma-2 item__icon" icon="fa:fa fa-microchip"></v-icon>
      <div class="item__label">{{ item.name }}</div>
    </v-card>

    <!-- Thêm mới -->
    <v-dialog v-model="createPopup" persistent width="768">
      <template v-slot:activator="{ props }">
        <v-btn class="ma-3" elevation="10" height="180" width="180" theme="dark" color="#001664" v-bind="props">
          <v-sheet class="d-flex justify-center flex-column align-center;" width="180" color="transparent" height="180">
            <div>
              <div style="font-size: 48px; font-weight: bolder">+</div>
              <div class="d-flex justify-center flex-wrap" style="width: 100%; font-weight: bold">
                Create New Device
              </div>
            </div>
          </v-sheet>
        </v-btn>
      </template>
      <v-card theme="light">
        <v-card-title>
          <span style="color: #000832; font-weight: bold" class="text-h5">Device Information</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field color="#000832" label="Name *" required v-model="objectDevice.name"></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-textarea color="#000832" label="Description" rows="4" v-model="objectDevice.description"></v-textarea>
              </v-col>
              <v-col cols="12" sm="6">
                <v-select color="#000832" :items="['Active', 'Unactive']" label="Status"
                  v-model="objectDevice.isActive"></v-select>
              </v-col>
              <v-col cols="12" sm="6">
                <small style="height: 80%" class="d-flex align-center justify-center text-center w-100"><b
                    style="color: red; margin-right: 4px">*</b> Indicates
                  required field</small>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="#000832" variant="text" @click="createPopup = false">
            Close
          </v-btn>
          <v-btn color="white" style="background-color: #001664" variant="text" @click="omClickCreate">
            Create
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

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
          <v-btn theme="dark" color="white" variant="text" @click="onDoConfirm()" style="background-color: #001664">
            Ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Card (sheet) thông báo kết quả -->
    <v-dialog v-model="resultCard">
      <v-sheet elevation="12" max-width="400" rounded="lg" width="100%" class="pa-4 text-center mx-auto">
        <div v-if="resultInfo.status == 0">
          <v-icon class="mb-5" color="error" icon="mdi-alert-circle" size="112"></v-icon>
          <h2 class="text-h5 mb-6">Fail!</h2>
        </div>
        <div v-else-if="resultInfo.status == 1">
          <v-icon class="mb-5" color="success" icon="mdi-check-circle" size="112"></v-icon>
          <h2 class="text-h5 mb-6">Success!</h2>
        </div>

        {{ resultInfo.message }}
        <br />
        <p class="mb-4 text-medium-emphasis text-body-2" v-if="resultInfo.linkHref">
          {{ resultInfo.linkPreNote }}
          <router-link :to="resultInfo.linkHref" class="text-decoration-none text-info">{{ resultInfo.linkTitle ?
            resultInfo.linkTitle : "Go to link." }}.
          </router-link>
        </p>
      </v-sheet>
    </v-dialog>

    <VtfLoading v-if="isShowLoading" />
  </v-sheet>
</template>

<script>
import axios from "axios";
import router from "@/routers";
import VtfLoading from "@/components/VtfLoading.vue";

export default {
  name: "ListDevice",
  components: {
    VtfLoading,
  },

  data: () => ({
    apiBaseURL: process.env.VUE_APP_API_BASE_URL,
    ListDevice: [],
    createPopup: false,
    token: localStorage.getItem("token"),
    confirmDialog: false,
    objectDevice: {
      userId: 0,
      name: undefined,
      description: "",
      isActive: "Active",
    },
    confirmInfo: {
      title: "",
      message: "",
    },
    confirmAction: 0, // 0: Unactive, 1: Delete, 2: Create
    resultCard: false,
    resultInfo: {
      status: null, // 0: Fail, 1: Success, 2: Warning, 3: Note
      message: "",
      linkPreNote: "",
      linkHref: "",
      linkTitle: "",
    },
    isShowLoading: false,
    // keySheetListDevice: false,
  }),

  methods: {
    async getListDevice() {
      this.isShowLoading = true;
      try {
        await axios
          .get(this.apiBaseURL + "Devices/exist", {
            headers: {
              Authorization: 'Bearer ' + this.token
            }
          })
          .then((response) => {
            this.ListDevice = response.data;
          })
          .catch((response) => {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      this.isShowLoading = false;
    },

    async getListDeviceUser() {
      this.isShowLoading = true;
      try {
        await axios
          .get(this.apiBaseURL + "Devices/userId?userId=" + this.userIdbyToken(), {
            headers: {
              Authorization: 'Bearer ' + this.token
            }
          })
          .then((response) => {
            this.ListDevice = response.data;
          })
          .catch((response) => {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      this.isShowLoading = false;
    },

    onClickDevice(id) {
      router.push("/devices/" + id);
    },

    userIdbyToken() {
      var logginToken = localStorage.getItem('token') || '';
      var _extractedToken = logginToken.split('.')[1];
      var _atobData = atob(_extractedToken.toString());
      var _finaldata = JSON.parse(_atobData);
      return _finaldata.userId
    },

    omClickCreate() {
      this.confirmInfo = {
        title: "Create New Device - Policy Notes",
        message: `When you create a new device, it is registered to the Brainwave System. 
        We will provide API information so that your device can collect your brainwave data 
        and send it to the system, you can monitor your data and the information that our system analyzes. 
        get from them. In addition to the above benefits, registering this device also means that you are willing 
        to share your personal brainwave data with our team, we are committed and ready to be responsible for ensuring 
        the safety of your device. integrity and security of all your data. We are always ready to make the necessary legal commitments. 
        Please click "OK" to confirm your consent to share your brainwave data and let us know that you are definitely up for our policies, 
        or click "CANCEL" then "CLOSE" to cancel this device creation procedure. These are important notes, 
        please read them carefully and follow the instructions!`,
      };
      this.confirmDialog = true;
      this.confirmAction = 2;
    },

    async createDevice() {
      this.objectDevice.userId = this.userIdbyToken()
      this.isShowLoading = true;
      try {
        if (this.objectDevice.isActive == "Unactive")
          this.objectDevice.isActive = false;
        else this.objectDevice.isActive = true;
        await axios
          .post(this.apiBaseURL + "Devices", this.objectDevice, {
            headers: {
              Authorization: 'Bearer ' + this.token
            }
          })
          .then((response) => {
            this.createPopup = false;
            this.resultInfo = {
              status: 1, // 0: Fail, 1: Success, 2: Warning, 3: Note
              message: "Created new device successfully.",
              linkPreNote: "You can access detailed information here:",
              linkHref: "/devices/" + response.data,
              linkTitle: "Go to this device.",
            };
            this.resultCard = true;
            this.$emit("onReloadListDevice");
            this.resetAfterConfirm();
            setTimeout(() => {
              this.resultCard = false;
            }, 6000);
          })
          .catch((err) => {
            console.log(err);
            this.isShowLoading = false;
            if (err.response.status != 400) this.createPopup = false;
            this.resultInfo = {
              status: 0, // 0: Fail, 1: Success, 2: Warning, 3: Note
              message: "Create new device failed.",
              linkPreNote: "",
              linkHref: "",
              linkTitle: "",
            };
            this.resultCard = true;
            setTimeout(() => {
              this.resultCard = false;
            }, 6000);
          });
      } catch (ex) {
        console.log(ex);
      }
      // this.isShowLoading = false;
    },

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

    onClickDeleteDevice(id, name) {
      this.confirmInfo = {
        title: "Delete Device",
        message: "Are you sure you want to delete device " + name + "?",
      };
      this.confirmDialog = true;
      this.confirmAction = 1;
      this.selectedDeviceId = id;
    },

    onCancelConfirm() {
      //Nếu là unactive thì hủy action unactive đó
      if (this.confirmAction == 0)
        this.ListDevice.find(
          (item) => item.id == this.selectedDeviceId
        ).isActive = true;
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
      else if (this.confirmAction == 2)
        this.createDevice();
      this.resetAfterConfirm();
    },

    async callToggleActiveDevice(id) {
      axios
        .patch(this.apiBaseURL + "Devices/" + id + "/active", {
          headers: {
            Authorization: 'Bearer ' + this.token
          }
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
              Authorization: 'Bearer ' + this.token
            }
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

  created() {
    var logginToken = localStorage.getItem('token') || '';
    var _extractedToken = logginToken.split('.')[1];
    var _atobData = atob(_extractedToken.toString());
    var _finaldata = JSON.parse(_atobData);
    var role = _finaldata.role
    console.log(role)
    if (role === "admin")
      this.getListDevice();
    else
      this.getListDeviceUser();
  },

  mounted() {
    this.$emit("onSetAppName", "Home - My Devices");
  },
};
</script>

<style scoped>
.btn {
  width: 100%;
  height: 100%;
  position: relative;
}

.item__toolbar {
  position: absolute;
  /* font-size: 40px; */
  display: flex;
  justify-content: space-between;
  padding: 2px;
  width: 100%;
  top: 0;
  height: 32px;
}

.item__icon {
  position: absolute;
  display: flex;
  font-size: 40px;
  align-items: center;
  justify-content: center;
  width: 100%;
  top: 15%;
  height: calc(50% - 32px);
}

.item__label {
  position: absolute;
  width: 100%;
  height: 45%;
  bottom: 0;
  display: flex;
  font-size: 18px;
  text-align: center;
  justify-content: center;
  padding: 4px 16px;
}
</style>