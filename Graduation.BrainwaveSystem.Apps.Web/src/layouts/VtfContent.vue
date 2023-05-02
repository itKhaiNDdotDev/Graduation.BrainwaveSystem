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
          <KAreaChart v-if="n==2" :key="areaGeneralChartKey" :propDatas="tgamExtractions.generals" :propLabels="timeStampList" />
          <!-- <KAreaChart v-if="n==2" :key="area8BandsChartKey" :propDatas="tgamExtractions.data8Bands" :propLabels="timeStampList" /> -->
          <KLineChart v-if="n==3" :key="lineChartKey" :propLabels="timeStampList" :propDatas="rawValues" />
          <div>{{ currentTime }}</div>
          <v-btn @click="n==3?getRawData:getTGAMExtractionData">Refresh</v-btn>
        </v-container>
      </v-window-item>
    </v-window>
  </v-card>
    <!-- <KAreaChart :propDatas="tgamExtractions.data8Bands" :propLabels="timeStampList" /> -->
</template>
<script>
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";
import KAreaChart from '@/components/KAreaChart.vue';
//import { md2 } from 'vuetify/blueprints'

export default {
  components: {
    KLineChart,
    KAreaChart
  },

  data: () => ({
    tab: null,
    timeStampList: [],
    rawValues: [],
    currentTime: "",
    lineChartKey: 0,
    // tgamExtractions: {
    //   generals: {
    //     poorQualities: [],
    //     attentiona: [],
    //     meditations: []
    //   },
    //   data8Bands: {
    //     deltas: [],
    //     thetas: [],
    //     alphas: [],
    //     lowBetas: [],
    //     midBetas: [],
    //     highBetas: [],
    //     gamas: [],
    //     uhfGamas: []
    //   }
    // }
    tgamExtractions: {
      generals: [ [], [], [] ],
      data8Bands: [ [], [], [], [], [], [], [], [] ]
    }
  }),

  methods: {
    getTGAMExtractionData() {
      axios.get('https://localhost:44321/api/DeviceDatas/bcb6bd84-8247-4cce-acb4-48487b9015bb/last5mins')
      .then((res) => {
        var tmp8Bands = [ [], [], [], [] ];
        var tmpTimeStamp = [];
        var tmpGenerals = [ [], [], [] ];

          res.data.generals.forEach((record, index) => {
            const dateTime = new Date(record.createdTime);
            const minutes = dateTime.getMinutes();
            const seconds = dateTime.getSeconds();
            if(index == res.data.generals.length) {
              const hh = dateTime.getHours();
              const dd = dateTime.getDay();
              const mm = dateTime.getMonth();
              const yyyy = dateTime.getFullYear();
              this.currentTime = `${hh.toString().padStart(2, "0")}:${minutes
                .toString().padStart(2, "0")} ${(dd+1).toString().padStart(2, "0")}/${(mm+1)
                .toString().padStart(2, "0")}/${yyyy.toString()}`;

            }
              tmpTimeStamp.push(
                `${minutes.toString().padStart(2, "0")}:${seconds
                  .toString()
                  .padStart(2, "0")}`
              );
            //this.timeStampList.fill("", this.timeStampList.length, this.timeStampList.length + record.values.length - 1);
            tmpGenerals[0].push(record.poorQuality);
            tmpGenerals[1].push(record.attention);
            tmpGenerals[2].push(record.meditation);
          });

          res.data.data8Bands.forEach((record) => {
            tmp8Bands[0].push(record.delta);
            tmp8Bands[1].push(record.theta);
            tmp8Bands[2].push(record.alpha);
          });

          this.timeStampList = tmpTimeStamp;
          this.tgamExtractions.generals = tmpGenerals;
          this.tgamExtractions.data8Bands = tmp8Bands;
          this.areaGeneralChartKey++;
          this.area8BandsChartKey++;
      }).catch((err) => {
          console.log(err);
        });
    },

    getRawData() {
      axios
        .get(
          "https://localhost:44321/api/DataRawEEGs/bcb6bd84-8247-4cce-acb4-48487b9015bb/last5min"
        )
        .then((res) => {
          var tmpValues = [];
          var tmpTimeStamp = [];
          res.data.forEach((record, index) => {
            const dateTime = new Date(record.recivedTime);
            const minutes = dateTime.getMinutes();
            const seconds = dateTime.getSeconds();
            if(index == res.data.length - 1) {
              const hh = dateTime.getHours();
              const dd = dateTime.getDay();
              const mm = dateTime.getMonth();
              const yyyy = dateTime.getFullYear();
              this.currentTime = `${hh.toString().padStart(2, "0")}:${minutes
                .toString().padStart(2, "0")} ${(dd+1).toString().padStart(2, "0")}/${(mm+1)
                .toString().padStart(2, "0")}/${yyyy.toString()}`;
              // thiếu labels do có vô cùng nhiều raw mà labels theo general là thêius
            }
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
          //console.log(this.rawValues);
          this.lineChartKey++;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },

  mounted() {
    this.getRawData();
    this.getTGAMExtractionData();
  },
};
</script>

<style lang="">
</style>