<template>
  <!-- class="bg-deep-purple", color="grey-darken-3", color="grey-darken-3", permanent -->
  <v-navigation-drawer
    color="#000832"
    width="276"
    v-model="drawer"
    :rail="rail"
    @click="rail = false"
    elevation="2"
    theme="dark"
  >
    <v-list-item
      height="56"
      style="background-color: #eeeeee; border-radius: 0"
      nav
      elevation="2"
    >
      <div class="app__title d-flex align-center">
        <div class="app__logo"></div>
        <div class="ml-2">Brainwave System</div>
      </div>
      <template v-slot:append>
        <v-btn
          v-if="!rail"
          variant="text"
          icon="mdi-chevron-left"
          @click.stop="rail = !rail"
          class="app__title"
        ></v-btn>
        <v-btn
          v-else
          variant="text"
          icon="mdi-chevron-right"
          @click.stop="rail = !rail"
          class="app__title"
        ></v-btn>
      </template>
    </v-list-item>
    <v-divider></v-divider>

    <!-- v-model:opened="open" -->
    <div v-if="token !== null">
      <v-list color="transparent" density="compact" nav>
      <!-- Home -->
      <router-link to="/devices" v-slot="{ href, navigate }">
        <v-list-item
          prepend-icon="mdi-home"
          title="Home"
          @click="navigate"
          :href="href"
          nav
        >
          <v-tooltip
            theme="light"
            v-if="rail"
            activator="parent"
            location="start"
          >
            {{ "Home" }}
          </v-tooltip>
        </v-list-item>
        <v-divider></v-divider>
      </router-link>

      <!-- Dah sách device -->
      <router-link
        v-for="item in listDevice"
        :key="item.id"
        :to="'/devices/' + item.id"
        v-slot="{ href, navigate }"
      >
        <!-- @mouseover="hoveredItem = item.id"
        @mouseout="hoveredItem = null"
        style="position: relative" -->
        <!-- Phần hiển thị khi rail mà hover vào -->
        <!-- <div ref="titleWrapperRef" v-if="hoveredItem === item.id && rail">
          <div class="title--hover">{{ item.name }}</div>
        </div> -->
        <!-- <v-list-group :value="item.name">
          <template v-slot:activator="{ props }"> -->
        <v-list-item
          v-bind="props"
          prepend-icon="fa:fa fa-microchip"
          :title="item.name"
          @click="navigate"
          :href="href"
          nav
        >
          <v-tooltip
            v-if="rail"
            theme="light"
            activator="parent"
            location="start"
          >
            {{ item.name }}
          </v-tooltip>
        </v-list-item>
        <!-- </template> -->
        <!-- <v-list-item
            v-for="(item, index) in DeviceSubTabs"
            :key="index"
            :title="item"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item> -->
        <!-- </v-list-group> -->
      </router-link>

      <!-- Tạo mới -->
      <!-- <v-list-item prepend-icon="mdi-add" title="Tạo mới"> -->
      <v-dialog theme="light" v-model="createPopup" persistent width="768">
        <template v-slot:activator="{ props }">
          <v-btn
            theme="dark"
            color="#001664"
            v-if="rail"
            v-bind="props"
            style="min-width: 32px !important"
          >
            <b style="font-size: 32px">+</b>
          </v-btn>
          <v-btn
            color="#001664"
            v-else
            v-bind="props"
            style="width: 100%; margin: 4px 0px"
            theme="dark"
          >
            <b style="font-size: 32px">+</b>&nbsp; Create New Device
          </v-btn>
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
                  <!-- type="password" -->
                  <!-- <v-text-field
                  label="Description"
                ></v-text-field> -->
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
                  <!-- <v-autocomplete
                  :items="['Skiing', 'Ice hockey', 'Soccer', 'Basketball', 'Hockey', 'Reading', 'Writing', 'Coding', 'Basejump']"
                  label="Interests"
                  multiple
                ></v-autocomplete> -->
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="#000832" variant="text" @click="createPopup = false">
              Close
            </v-btn>
            <v-btn
              color="white"
              style="background-color: #001664"
              variant="text"
              @click="onClickCreate"
            >
              Save
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <!-- </v-list-item> -->
    </v-list>
    </div>

    <!-- <div class="pa-2">
      <v-btn @click="onClickCreate" block>
        <b style="font-size: 32px">+</b>&nbsp; Create New Device
      </v-btn>
    </div> -->

    <template v-slot:append>
      <div class="pa-2">
        <v-btn color="#001664" block class="d-flex justify-center">
          <v-icon class="ma-2" icon="mdi-cog"></v-icon>
          <span v-if="!rail">Settings</span>
        </v-btn>
      </div>
    </template>

    <!-- Popup Confirm -->
    <v-dialog v-model="confirmDialog" theme="light" persistent width="auto">
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
    <v-dialog theme="light" v-model="resultCard">
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
  </v-navigation-drawer>
  <!-- <CreateDevicePopup class="pa-2" /> -->
  <!-- <create-device-popup></create-device-popup> -->
</template>

<script>
// import CreateDevicePopup from "@/views/CreateDevicePopup.vue";
import axios from "axios";

export default {
  comments: {
    // CreateDevicePopup,
  },

  data: () => ({
    drawer: true,
    rail: false,
    // DeviceSubTabs: ["General", "TGAM Extraction", "Raw EEG Data"],
    apiBaseURL: process.env.VUE_APP_API_BASE_URL,
    token : localStorage.getItem("token"),
    createPopup: false,
    objectDevice: {
      userId: 0,
      name: undefined,
      description: "",
      isActive: "Active",
    },
    listDevice: [],
    // hoveredItem: null,
    resultCard: false,
    resultInfo: {
      status: null, // 0: Fail, 1: Success, 2: Warning, 3: Note
      message: "",
      linkPreNote: "",
      linkHref: "",
      linkTitle: "",
    },
    confirmDialog: false,
    confirmInfo: {
      title: "",
      message: "",
    },
    confirmAction: 2, // 0: Unactive, 1: Delete, 2: Create
  }),

  methods: {
    getListActiveDevice() {
      this.$emit("onShowLoading");
      try {
        axios
          .get(this.apiBaseURL + "Devices/exist", {
            headers: {
              Authorization: 'Bearer ' + this.token
            }
          })
          .then((response) => {
            this.listDevice = response.data;
          })
          .catch((response) => {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      this.$emit("onHideLoading");
    },

    getListActiveDeviceUser() {
      this.$emit("onShowLoading");
      try {
        axios
          .get(this.apiBaseURL + "Devices/userId?userId=" + this.userIdbyToken(), {
            headers: {
              Authorization: 'Bearer ' + this.token
            }
          })
          .then((response) => {
            this.listDevice = response.data;
          })
          .catch((response) => {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      this.$emit("onHideLoading");
    },

    onClickCreate() {
        
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
      this.$emit("onShowLoading");
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
            this.resultInfo = {
              status: 1, // 0: Fail, 1: Success, 2: Warning, 3: Note
              message: "Created new device successfully.",
              linkPreNote: "You can access detailed information here:",
              linkHref: "/devices/" + response.data,
              linkTitle: "Go to this device.",
            };
            this.resultCard = true;
            this.confirmDialog = false;
            this.createPopup = false;
            this.objectDevice = {
              name: undefined,
              description: "",
              isActive: "Active",
            };
            this.$emit("onReloadListDevice");
            setTimeout(() => {
              this.resultCard = false;
            }, 6000);
          })
          .catch((err) => {
            console.log(err);
            this.$emit("onHideLoading");
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
      // this.$emit("onHideLoading");
    },

    userIdbyToken() {
      var logginToken = localStorage.getItem('token') || '';
      var _extractedToken = logginToken.split('.')[1];
      var _atobData = atob(_extractedToken.toString());
      var _finaldata = JSON.parse(_atobData);
      return _finaldata.userId
    },

    onCancelConfirm() {
      this.confirmAction = 2;
      this.confirmDialog = false;
      this.confirmInfo = { title: "", message: "" };
    },

    onDoConfirm() {
      this.createDevice();
    },

    // updateTitlePosition() {
    //   if (this.$refs.titleWrapperRef && this.$refs.titleWrapperRef.getBoundingClientRect) {
    //     const sidebarRect = 100;
    //     const titleWrapper = this.$refs.titleWrapperRef;
    //     const titleWidth = titleWrapper.offsetWidth;
    //     const titleHeight = titleWrapper.offsetHeight;
    //     const titleTop = sidebarRect.top;
    //     const titleLeft = sidebarRect.left + sidebarRect.width;

    //     titleWrapper.style.width = `${titleWidth}px`;
    //     titleWrapper.style.height = `${titleHeight}px`;
    //     titleWrapper.style.top = `${titleTop}px`;
    //     titleWrapper.style.left = `${titleLeft}px`;
    //   }
    // },
  },

  created() {
    var logginToken = localStorage.getItem('token') || '';
    var _extractedToken = logginToken.split('.')[1];
    var _atobData = atob(_extractedToken.toString());
    var _finaldata = JSON.parse(_atobData);
    var role = _finaldata.role
    if (role === "admin")
    this.getListActiveDevice();
    else
    this.getListActiveDeviceUser();
  },

  // mounted() {
  //   this.updateTitlePosition();
  //   window.addEventListener('resize', this.updateTitlePosition);
  // },

  // unmounted() {
  //   window.removeEventListener('resize', this.updateTitlePosition);
  // },
};
</script>

<style scoped>
.app__logo {
  background-image: url(../assets/logo.png);
  background-size: contain;
  background-position: center;
  width: 56px;
  height: 48px;
}

.app__title {
  font-size: 18px;
  font-weight: bolder;
  /* color: #001664; */
  color: #000848;
}

.nav__drawer {
  background-color: #000832;
  height: 100vh;
}

a {
  text-decoration: none;
  color: inherit;
}

/* .title--hover {
  position: fixed;
  width: 100px;
  padding: 16px;
  background-color: red;
  z-index: 999;
  border-bottom: 1px solid #dddddd;
  border-right: 1px solid #dddddd;
} */

.router-link-active {
  color: #ddff44;
}
.router-link-active .v-list-item {
  background-color: #00166484;
  border-radius: 0;
}
.router-link-active .v-list-item::before {
  width: 4px;
  height: 100%;
  background-color: red;
  position: absolute;
  top: 0;
  left: 100%;
}
</style>