<!--<template>
  <div>aaa</div>
  <v-icon icon="mdi-home" />
  <v-icon icon="fas fa-plus" />
  <v-icon icon="mdi:mdi-minus" />
</template>-->
<template>
  <v-footer
    class="bg-blue-grey-darken-4 text-center d-flex flex-column"
    elevation="2"
  >
    <div>
      <!-- <v-btn
        v-for="item in linkIcons"
        :key="item.icon"
        class="mx-4"
        :icon="item.icon"
        variant="text"
        v-bind:to="item.link"
        tag="router-link"
      ></v-btn> -->
      <a
        v-for="item in linkIcons"
        :key="item.icon"
        class="mx-4"
        :href="item.link"
        target="_blank"
        style="ext-decoration: none; color: inherit;"
      >
        <v-btn :icon="item.icon" variant="text"></v-btn>
      </a>
    </div>

    <div class="pt-0">
      Collect, Storage, Analysis, Extract and Predict information form your
      brainwave EEG data.
    </div>
    <v-divider></v-divider>
    <div>{{ new Date().getFullYear() }} — <strong>KhaiND</strong></div>
  </v-footer>
</template>

<script>
export default {
  data: () => ({
    linkIcons: [
      { icon: "mdi-github", link: "https://github.com/itKhaiNDdotDev" },
      { icon: "mdi-facebook", link: "https://www.facebook.com/itKhaiND.Dev" },
      { icon: "mdi-linkedin", link: "" },
    ],
  }),
};
</script>

<style>
</style>