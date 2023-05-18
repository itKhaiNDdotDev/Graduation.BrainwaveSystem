<template>
  <v-card>
    <v-tabs v-model="tab" color="deep-purple-accent-4" align-tabs="center">
      <v-tab :value="1">General</v-tab>
      <v-tab :value="2">TGAM Extraction</v-tab>
      <v-tab :value="3">Raw EEG Data</v-tab>
      <v-tab :value="4">Models Result</v-tab>
    </v-tabs>
    <v-window v-model="tab">
      <v-window-item v-for="n in 4" :key="n" :value="n">
        <v-container fluid>
          <TgamExtractionTab v-if="n==2"/>
          <RawEEGDataTab v-if="n==3"/>
        </v-container>
      </v-window-item>
    </v-window>
  </v-card>
</template>
<script>
//import { md2 } from 'vuetify/blueprints'
import TgamExtractionTab from '@/views/TgamExtractionTab.vue';
import RawEEGDataTab from '@/views/RawEEGDataTab.vue';

export default {
  components: {
    TgamExtractionTab,
    RawEEGDataTab
  },

  data: () => ({
    tab: null,
  }),

  created() {
  },
};
</script>

<style lang="">
</style>