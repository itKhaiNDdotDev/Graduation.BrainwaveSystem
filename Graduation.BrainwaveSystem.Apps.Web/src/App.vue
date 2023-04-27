<template>
  <v-app>
    <v-layout>
       <!-- <v-system-bar color="grey-darken-3"></v-system-bar> -->

      <!-- <v-navigation-drawer
        color="grey-darken-3"
        permanent
        width="256"
      ></v-navigation-drawer> -->
      <VtfSideBar/>

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

      <v-main>
        <VtfContent/>
      </v-main>
    </v-layout>
    <VtfFooter/>
  </v-app>
</template>

<script>
import VtfAppBar from './layouts/VtfAppBar.vue';
import VtfContent from './layouts/VtfContent.vue';
import VtfSideBar from './layouts/VtfSideBar.vue';
import VtfFooter from "./layouts/VtfFooter.vue";

export default {
  name: "App",
  components: {
    VtfFooter,
    VtfAppBar,
    VtfSideBar,
    VtfContent
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
