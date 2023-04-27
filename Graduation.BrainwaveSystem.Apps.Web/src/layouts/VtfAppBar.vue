<template>
  <v-app-bar class="bg-grey-darken-4" height="64" flat>
    <v-toolbar title="Device: KhaiND Test" color="grey-lighten-3">
      <v-btn stacked>
        <v-badge color="error" dot>
          <v-icon icon="mdi-newspaper-variant-outline"></v-icon>
        </v-badge>

        News
      </v-btn>

      <v-btn stacked>
        <v-badge content="2" color="error">
          <v-icon>mdi-bell-outline</v-icon>
        </v-badge>
        Notifications
      </v-btn>

      <v-btn stacked variant="tonal">
        <v-icon icon="mdi-login"></v-icon>

        Login
      </v-btn>
    </v-toolbar>
  </v-app-bar>
</template>
<script>
export default {};
</script>
<style lang="">
</style>