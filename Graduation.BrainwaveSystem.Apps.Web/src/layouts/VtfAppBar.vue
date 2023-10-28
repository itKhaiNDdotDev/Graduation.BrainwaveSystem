<template>
  <v-app-bar app fixed class="bg-grey-darken" height="56" flat elevation="2">
    <v-toolbar :title="appName ? appName : ' '">
      <div>
        <v-btn stacked>
          <v-badge content="2" color="error">
            <v-icon color="#000832">mdi-bell</v-icon>
          </v-badge>
        </v-btn>
        <v-btn stacked v-bind="props">
          <div class="d-flex text-center justify-center" style="align-items: center;">
            <img src="../assets/anonymous.png" style="width: 40px; height: 40px;border-radius: 50px; object-fit: cover;" />
          <p style="padding: 5px;">{{ token !== null ? namebyToken().split(" ")[namebyToken().split(" ").length - 1] : "" }}</p>
          </div>
        </v-btn>
        <v-btn stacked variant="tonal" @click="onClickLogout" v-bind="props">
          LOG OUT
        </v-btn>
      </div>

      <v-dialog theme="light" v-model="resultCard">
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
    </v-toolbar>
  </v-app-bar>
</template>

<script>
export default {
  props: ["appName"],
  data: () => ({
    apiBaseURL: process.env.VUE_APP_API_BASE_URL,
    rail: false,
    token: localStorage.getItem("token"),
  }),

  methods: {
    onClickLogout() {
      this.hideNonLogin = true,
        localStorage.clear(),
        window.location.reload()
    },

    namebyToken() {
      var logginToken = localStorage.getItem('token') || '';
      var _extractedToken = logginToken.split('.')[1];
      var _atobData = atob(_extractedToken.toString());
      var _finaldata = JSON.parse(_atobData);
      return _finaldata.name
    },
  },
};
</script>

<style scoped>
</style>