<!--<template>
  <div>aaa</div>
  <v-icon icon="mdi-home" />
  <v-icon icon="fas fa-plus" />
  <v-icon icon="mdi:mdi-minus" />
</template>-->
<template>
  <v-footer
    class="bg-grey-darken-4 text-center d-flex flex-column"
  >
    <div>
      <v-btn
        v-for="icon in icons"
        :key="icon"
        class="mx-4"
        :icon="icon"
        variant="text"
      ></v-btn>
    </div>

    <div class="pt-0">
      Cllect, Storage, analys and predict information form your brainwave data. 
    </div>

    <v-divider></v-divider>

    <div>
      {{ new Date().getFullYear() }} — <strong>KhaiND</strong>
    </div>
  </v-footer>
</template>

<script>
  export default {
    data: () => ({
      icons: [
        'mdi-facebook',
        'mdi-twitter',
        'mdi-linkedin',
        'mdi-instagram',
      ],
    }),
  }
</script>

<style>
</style>