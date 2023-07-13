<template>
  <v-app>
    <v-layout>
       <!-- <v-system-bar color="grey-darken-3"></v-system-bar> -->

      <!-- <v-navigation-drawer
        color="grey-darken-3"
        permanent
        width="256"
      ></v-navigation-drawer> -->
      <VtfSideBar :key="sidebarKey" @onRefresh="onRefresh"/>

      <!-- <v-app-bar
        color="grey"
        height="64"
        flat
      ></v-app-bar> -->
      <VtfAppBar/>

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
        <router-view :key="mainViewKey" @onRefresh="onRefresh"></router-view>
      </v-main>
    </v-layout>
    <VtfFooter/>
  </v-app>
</template>

<script>
import VtfAppBar from './layouts/VtfAppBar.vue';
// import VtfContent from './layouts/VtfContent.vue';
import VtfSideBar from './layouts/VtfSideBar.vue';
import VtfFooter from "./layouts/VtfFooter.vue";

export default {
  name: "App",
  components: {
    VtfFooter,
    VtfAppBar,
    VtfSideBar,
    // VtfContent
  },

  data() {
    return {
      sidebarKey: 0,
      mainViewKey: 0
    }
  },

  methods: {
    onRefresh() {
      console.log("Refresh");
      this.sidebarKey++;
      this.mainViewKey++;
    }
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
