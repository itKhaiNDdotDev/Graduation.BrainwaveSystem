<template>
  <v-overlay :model-value="overlay" class="align-center justify-center" persistent>
    <v-progress-circular
      color="#001664"
      indeterminate
      size="64"
    ></v-progress-circular>
  </v-overlay>
</template>

<script>
  export default {
    name:"VtfLoading",

    data: () => ({
      overlay: true,
    }),
  }
</script>