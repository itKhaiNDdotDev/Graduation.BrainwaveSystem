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
          <KLineChart :key="lineChartKey" :propLabels="timeStampList" :propDatas="rawValues" />
          <div>{{ currentTime }}</div>
          <v-btn @click="getRawData">Refresh</v-btn>
        </v-container>
      </v-window-item>
    </v-window>
  </v-card>
</template>
<script>
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";
//import { md2 } from 'vuetify/blueprints'

export default {
  components: {
    KLineChart,
  },

  data: () => ({
    tab: null,
    timeStampList: [],
    rawValues: [],
    currentTime: "",
    lineChartKey: 0
  }),

  methods: {
    getRawData() {
      axios
        .get(
          "https://localhost:44321/api/DataRawEEGs/bcb6bd84-8247-4cce-acb4-48487b9015bb/last5min"
        )
        .then((res) => {
          var tmpValues = [];
          var tmpTimeStamp = [];
          res.data.forEach((record) => {
            const dateTime = new Date(record.recivedTime);
            const minutes = dateTime.getMinutes();
            const seconds = dateTime.getSeconds();
            const hh = dateTime.getHours();
            const dd = dateTime.getDay();
            const mm = dateTime.getMonth();
            const yyyy = dateTime.getFullYear();
            this.currentTime = `${hh.toString().padStart(2, "0")}:${minutes
              .toString().padStart(2, "0")} ${(dd+1).toString().padStart(2, "0")}/${(mm+1)
              .toString().padStart(2, "0")}/${yyyy.toString()}`;
            tmpTimeStamp.push(
              `${minutes.toString().padStart(2, "0")}:${seconds
                .toString()
                .padStart(2, "0")}`
            );
            //this.timeStampList.fill("", this.timeStampList.length, this.timeStampList.length + record.values.length - 1);
            tmpValues.push(record.value);
          });
          this.timeStampList = tmpTimeStamp;
          this.rawValues = tmpValues;
          //console.log(this.timeStampList);
          console.log(this.rawValues);
          this.lineChartKey++;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },

  mounted() {
    this.getRawData();
  },
};
</script>

<style lang="">
</style>