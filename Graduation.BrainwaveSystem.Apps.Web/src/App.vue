<template>
  <v-app>
    <v-layout v-if="token !== null">
      <!-- <v-system-bar color="grey-darken-3"></v-system-bar> -->

      <!-- <v-navigation-drawer
        color="grey-darken-3"
        permanent
        width="256"
      ></v-navigation-drawer> -->
      <VtfSideBar
        ref="sideBar"
        @onReloadListDevice="onReloadListDevice"
        @onShowLoading="isShowLoading = true"
        @onHideLoading="isShowLoading = false"
      />

      <!-- <v-app-bar
        color="grey"
        height="64"
        flat
      ></v-app-bar> -->
      <VtfAppBar :appName="appName" />

      <!-- <v-navigation-drawer
        color="grey-lighten-1"
        location="right"
        permanent
        width="150"
      ></v-navigation-drawer> -->

      <!-- <v-app-bar
        color="grey-lighten-2"
        flat
        height="48"
        location="bottom"
      ></v-app-bar> -->

      <!-- <VtfFooter /> -->

      <v-main style="min-height: 90vh">
        <!-- <VtfContent/> -->
        <!-- :key="mainViewKey"  -->
        <!-- <router-view ref="mainContent" @onReloadListDevice="onReloadListDevice"></router-view> -->
        <router-view v-slot="{ Component }">
          <component
            :key="$route.fullPath"
            ref="mainContent"
            :is="Component"
            @onReloadListDevice="onReloadListDevice"
            @onSetAppName="onSetAppName"
        /></router-view>
      </v-main>
    </v-layout>
    <v-layout v-else>
      <VtfLogin> </VtfLogin>
    </v-layout>

    <VtfFooter />

    <VtfLoading v-if="isShowLoading" />
  </v-app>
</template>

<script>
import VtfAppBar from "./layouts/VtfAppBar.vue";
// import VtfContent from './layouts/VtfContent.vue';
import VtfSideBar from "./layouts/VtfSideBar.vue";
import VtfFooter from "./layouts/VtfFooter.vue";
import VtfLoading from "@/components/VtfLoading.vue";
import VtfLogin from "./layouts/VtfLogin.vue";

export default {
  name: "App",
  components: {
    VtfFooter,
    VtfAppBar,
    VtfSideBar,
    // VtfContent,
    VtfLoading,
    VtfLogin,
  },

  data() {
    return {
      token: localStorage.getItem("token"),
      sidebarKey: 0,
      mainViewKey: 0,
      isShowLoading: false,
      appName: null,
    };
  },

  mounted() {},

  methods: {
    onRefresh() {
      // this.sidebarKey++;
      // this.mainViewKey++;
    },

    // async onReloadListDevice() {
    //   console.log("emit");
    //   if (typeof this.$refs.sideBar.getListActiveDevice === "function")
    //   {
    //     if(this.token === "admin")
    //       await this.$refs.sideBar.getListActiveDevice();
    //       else
    //         await this.$refs.sideBar.getListActiveDeviceUser();
    //   }
    //   if (typeof this.$refs.mainContent.getListDevice === "function")
    //   {
    //     if(this.token === "admin")
    //       await this.$refs.mainContent.getListDevice();
    //       else
    //         await this.$refs.mainContent.getListDeviceUser();
    //   }
    // },
    async onReloadListDevice() {
      var logginToken = localStorage.getItem("token") || "";
      var _extractedToken = logginToken.split(".")[1];
      var _atobData = atob(_extractedToken.toString());
      var _finaldata = JSON.parse(_atobData);
      if (typeof this.$refs.sideBar.getListActiveDevice === "function") {
        if (_finaldata.role === "admin")
          await this.$refs.sideBar.getListActiveDevice();
        else await this.$refs.sideBar.getListActiveDeviceUser();
      }
      if (typeof this.$refs.mainContent.getListDevice === "function") {
        if (_finaldata.role === "admin")
          await this.$refs.mainContent.getListDevice();
        else await this.$refs.mainContent.getListDeviceUser();
      }
    },

    onSetAppName(value) {
      this.appName = value;
    },
  },
};
</script>

<style>
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
} */
</style>
