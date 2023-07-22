<template>
  <v-app-bar app fixed class="bg-grey-darken" height="56" flat elevation="2">
    <v-toolbar :title="appName? appName : ' '">
      <v-btn stacked>
        <v-badge content="2" color="error">
          <v-icon color="#000832">mdi-bell</v-icon>
        </v-badge>
      </v-btn>

      <v-btn stacked variant="tonal">
        Login
      </v-btn>
    </v-toolbar>
  </v-app-bar>
</template>

<script>
export default {
  props: ["appName"]
};
</script>

<style lang="">
</style>