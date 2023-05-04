<template>
  <v-card>
    <v-tabs v-model="tab" color="deep-purple-accent-4" align-tabs="center">
      <v-tab :value="1">General</v-tab>
      <v-tab :value="2">TMGA Results</v-tab>
      <v-tab :value="3">Raw Data</v-tab>
    </v-tabs>
    <v-window v-model="tab">
      <v-window-item v-for="n in 3" :key="n" :value="n">
        <!-- <v-container fluid>
          <v-row>
            <v-col
              v-for="i in 6"
              :key="i"
              cols="12"
              md="4"
            >
              <v-img
                :src="`https://picsum.photos/500/300?image=${i * n * 5 + 10}`"
                :lazy-src="`https://picsum.photos/10/6?image=${i * n * 5 + 10}`"
                aspect-ratio="1"
              ></v-img>
            </v-col>
          </v-row>
        </v-container> -->
        <!-- <v-sheet
          class="v-sheet--offset mx-auto"
          color="cyan"
          elevation="12"
          max-width="calc(100% - 32px)"
        >
          <v-sparkline
            :labels="labels"
            :value="values"
            color="white"
            line-width="2"
            padding="16"
          ></v-sparkline>
        </v-sheet> -->
        <KAreaChart v-if="!IsProcessing" :labelProps="labels" :dataProps="values"/>
        <div v-else>Loading</div>
      </v-window-item>
      <input type="text" v-bind="inputLabel">
      <input type="text" v-bind="inputVal1">
      <input type="text" v-bind="inputVal2">
      <button @click="onClickPush">Push</button>
    </v-window>
  </v-card>
</template>
<script>
import KAreaChart from '@/components/KAreaChart.vue';
//import { md2 } from 'vuetify/blueprints'

export default {
  components: {
    KAreaChart
  },

  data() {
    return {
      tab: null,
    labels: ["12am", "3am", "6am", "9am", "12pm", "3pm", "6pm", "9pm"],
    values:[ [200, 675, 410, 390, 310, 460, 250, 240], [410, 390, 310, 460, 120, 130, 520, 400] ],
    inputLabel: "",
    inputVal1: "",
    inputVal2: "",
    IsProcessing: false
    }
  },

  methods: {
    pushData(newLabel, val1, val2) {
      console.log(val1 + val2)
      this.IsProcessing = true;
      this.labels.push(newLabel);
      //this.values[0].push(val1);
      //this.values[1].push(val2);
      this.IsProcessing = false;
    },

    onClickPush() {
      console.log("IN EVENT")
      this.pushData(this.inputLabel, parseInt(this.inputVal1), parseInt(this.inputVal2));
    }
  },
};
</script>

<style lang="">
</style>