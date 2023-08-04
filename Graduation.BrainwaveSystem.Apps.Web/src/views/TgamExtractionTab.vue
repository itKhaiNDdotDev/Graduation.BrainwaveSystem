<template>
  <div>
    <v-card class="mx-auto">
      <!-- <v-container fluid>
        <v-row dense>
          <v-col cols="8">
            <v-card>
              <KAreaChart
                v-if="tgamExtractions.generals[0].data"
                :propDatas="tgamExtractions.generals"
                :propLabels="timeStampList"
                @onLoadData="getTGAMExtractionData"
              />
            </v-card>
          </v-col>
          <v-col cols="4">
            <v-row>
              <v-card class="w-100">
                <KAreaChart
                  v-if="tgamExtractions.generals[0].data"
                  :propDatas="tgamExtractions.generals"
                  :propLabels="timeStampList"
                  @onLoadData="getTGAMExtractionData"
                />
                <v-spacer></v-spacer>
                <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: 213.11 - MAE: 201
              </v-card>
            </v-row>
            <v-row>
              <v-card class="w-100">
                <KAreaChart
                  v-if="tgamExtractions.generals[0].data"
                  :propDatas="tgamExtractions.generals"
                  :propLabels="timeStampList"
                  @onLoadData="getTGAMExtractionData"
                />
                <v-spacer></v-spacer>
                <b>&nbsp;&nbsp;Prediction LSTM: </b> - RMSE: 213.11 - MAE: 201
              </v-card>
            </v-row>
          </v-col>
        </v-row>
        General Results - <b>{{ currentTime }}</b>
      </v-container> -->
      <v-card class="mx-auto">
        <v-card style="width: 66%; float: left">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            @onLoadData="getTGAMExtractionData"
          />
        </v-card>
        <v-card style="width: 33%; float: left">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            @onLoadData="getTGAMExtractionData"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
        <v-card style="width: 33%; float: left">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            @onLoadData="getTGAMExtractionData"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction LSTM: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
        <div style="margin-bottom: 16px">
          General Results - <b>{{ currentTime }}</b>
        </div>
      </v-card>
    </v-card>

    <!-- <KAreaChart
      :propDatas="tgamExtractions.data8Bands"
      :propLabels="timeStampList"
      v-if="tgamExtractions.data8Bands[0].data"
    /> -->

    <v-card class="mx-auto">
      <v-card style="width: 66%; float: left">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          @onLoadData="getTGAMExtractionData"
        />
      </v-card>
      <v-card style="width: 33%; float: left">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          @onLoadData="getTGAMExtractionData"
        />
      </v-card>
      <v-card style="width: 33%; float: left">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          @onLoadData="getTGAMExtractionData"
        />
      </v-card>
    </v-card>
  </div>
</template>

<script>
import KAreaChart from "@/components/KAreaChart.vue";
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";

export default {
  components: {
    KAreaChart,
    KLineChart,
  },

  data() {
    return {
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      timeStampList: [],
      currentTime: "",
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
        generals: [{}, {}, {}],
        data8Bands: [{}, {}, {}, {}, {}, {}, {}, {}],
      },
    };
  },
  props: ["deviceId"],

  methods: {
    getTGAMExtractionData() {
      axios
        .get(this.apiBaseURL + "DeviceDatas/" + this.deviceId + "/LastMin")
        .then((res) => {
          var tmp8Bands = [[], [], [], [], [], [], [], []];
          var tmpTimeStamp = [];
          var tmpGenerals = [[], [], []];

          res.data.generals.forEach((record, index) => {
            const dateTime = new Date(record.createdTime);
            const minutes = dateTime.getMinutes();
            const seconds = dateTime.getSeconds();
            if (index == res.data.generals.length - 1) {
              const hh = dateTime.getHours();
              const dd = dateTime.getDay();
              const mm = dateTime.getMonth();
              const yyyy = dateTime.getFullYear();
              this.currentTime = `${hh.toString().padStart(2, "0")}:${minutes
                .toString()
                .padStart(2, "0")} ${(dd + 1).toString().padStart(2, "0")}/${(
                mm + 1
              )
                .toString()
                .padStart(2, "0")}/${yyyy.toString()}`;
            }
            tmpTimeStamp.push(
              `${minutes.toString().padStart(2, "0")}:${seconds
                .toString()
                .padStart(2, "0")}`
            );
            //this.timeStampList.fill("", this.timeStampList.length, this.timeStampList.length + record.values.length - 1);
            tmpGenerals[0].push(record.attention);
            tmpGenerals[1].push(record.meditation);
            tmpGenerals[2].push(record.poorQuality);
          });

          res.data.data8Bands.forEach((record) => {
            tmp8Bands[0].push(record.delta);
            tmp8Bands[1].push(record.theta);
            tmp8Bands[2].push(record.alpha);
            tmp8Bands[3].push(record.lowBeta);
            tmp8Bands[4].push(record.midBeta);
            tmp8Bands[5].push(record.highBeta);
            tmp8Bands[6].push(record.gama);
            tmp8Bands[7].push(record.uhfGama);
          });

          this.timeStampList = tmpTimeStamp;

          for (let i = 0; i < 3; i++) {
            this.tgamExtractions.generals[i].data = tmpGenerals[i];
          }

          for (let i = 0; i < 8; i++) {
            this.tgamExtractions.data8Bands[i].data = tmp8Bands[i];
          }

          this.configGeneralsChart();
          this.config8BandsChart();

          console.log("On GetTgamExtracion");
          console.log(this.tgamExtractions);
        })
        .catch((err) => {
          console.log(err);
        });
    },

    configGeneralsChart() {
      this.tgamExtractions.generals[0].lblName = "Attention";
      this.tgamExtractions.generals[0].bgColor = "#008000";
      this.tgamExtractions.generals[1].lblName = "Meditation";
      this.tgamExtractions.generals[1].bgColor = "#CC0000";
      this.tgamExtractions.generals[2].lblName = "Poor Quality";
      this.tgamExtractions.generals[2].bgColor = "#FFFF00";
    },

    config8BandsChart() {
      this.tgamExtractions.data8Bands[0].lblName = "Delta";
      this.tgamExtractions.data8Bands[0].bgColor = "#880000";
      this.tgamExtractions.data8Bands[0].lineThickness = 2;

      this.tgamExtractions.data8Bands[1].lblName = "Theta";
      this.tgamExtractions.data8Bands[1].bgColor = "#C71585";
      this.tgamExtractions.data8Bands[1].lineThickness = 2;

      this.tgamExtractions.data8Bands[2].lblName = "Alpha";
      this.tgamExtractions.data8Bands[2].bgColor = "#CC8800";
      this.tgamExtractions.data8Bands[2].lineThickness = 2;

      this.tgamExtractions.data8Bands[3].lblName = "Low Beta";
      this.tgamExtractions.data8Bands[3].bgColor = "#00AA00";
      this.tgamExtractions.data8Bands[3].lineThickness = 2;

      this.tgamExtractions.data8Bands[4].lblName = "Midle Beta";
      this.tgamExtractions.data8Bands[4].bgColor = "#004400";
      this.tgamExtractions.data8Bands[4].lineThickness = 2;

      this.tgamExtractions.data8Bands[5].lblName = "High Beta";
      this.tgamExtractions.data8Bands[5].bgColor = "#0220AA";
      this.tgamExtractions.data8Bands[5].lineThickness = 2;

      this.tgamExtractions.data8Bands[6].lblName = "Gmaa";
      this.tgamExtractions.data8Bands[6].bgColor = "#000044";
      this.tgamExtractions.data8Bands[6].lineThickness = 2;

      this.tgamExtractions.data8Bands[7].lblName = "UHF Gama";
      this.tgamExtractions.data8Bands[7].bgColor = "#000000";
      this.tgamExtractions.data8Bands[7].lineThickness = 2;
    },
  },

  created() {
    this.getTGAMExtractionData();
    // axios
    //     .get(
    //       "https://localhost:44321/api/DeviceDatas/bcb6bd84-8247-4cce-acb4-48487b9015bb/LastMin"
    //     )
    //     .then((res) => {
    //       var tmp8Bands = [[], [], [], [], [], [], [], []];
    //       var tmpTimeStamp = [];
    //       var tmpGenerals = [[], [], []];

    //       res.data.generals.forEach((record, index) => {
    //         const dateTime = new Date(record.createdTime);
    //         const minutes = dateTime.getMinutes();
    //         const seconds = dateTime.getSeconds();
    //         if (index == res.data.generals.length - 1) {
    //           const hh = dateTime.getHours();
    //           const dd = dateTime.getDay();
    //           const mm = dateTime.getMonth();
    //           const yyyy = dateTime.getFullYear();
    //           this.currentTime = `${hh.toString().padStart(2, "0")}:${minutes
    //             .toString()
    //             .padStart(2, "0")} ${(dd + 1).toString().padStart(2, "0")}/${(
    //             mm + 1
    //           )
    //             .toString()
    //             .padStart(2, "0")}/${yyyy.toString()}`;
    //         }
    //         tmpTimeStamp.push(
    //           `${minutes.toString().padStart(2, "0")}:${seconds
    //             .toString()
    //             .padStart(2, "0")}`
    //         );
    //         //this.timeStampList.fill("", this.timeStampList.length, this.timeStampList.length + record.values.length - 1);
    //         tmpGenerals[0].push(record.poorQuality);
    //         tmpGenerals[1].push(record.attention);
    //         tmpGenerals[2].push(record.meditation);
    //       });

    //       res.data.data8Bands.forEach((record) => {
    //         tmp8Bands[0].push(record.delta);
    //         tmp8Bands[1].push(record.theta);
    //         tmp8Bands[2].push(record.alpha);
    //         tmp8Bands[3].push(record.lowBeta);
    //         tmp8Bands[4].push(record.midBeta);
    //         tmp8Bands[5].push(record.highBeta);
    //         tmp8Bands[6].push(record.gama);
    //         tmp8Bands[7].push(record.uhfGama);
    //       });

    //       this.timeStampList = tmpTimeStamp;

    //       for (let i = 0; i < 3; i++) {
    //         this.tgamExtractions.generals[i].data = tmpGenerals[i];
    //       }

    //       for (let i = 0; i < 8; i++) {
    //         this.tgamExtractions.data8Bands[i].data = tmp8Bands[i];
    //       }

    //       this.configGeneralsChart();
    //       this.config8BandsChart();

    //       console.log(this.tgamExtractions);
    //     })
    //     .catch((err) => {
    //       console.log(err);
    //     });
    // console.log("Parrent Created")
  },

  mounted() {
    console.log("Parrent Mounted");
  },
};
</script>

<style lang="">
</style>