<template>
  <div>
    <v-card class="mx-auto">
      <v-card class="mx-auto" style="border-top: 1px solid #001664;">
        <v-card style="width: 68%; float: left; border-right: 1px solid #001664;">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            @onLoadData="getTGAMExtractionData"
          />
          <div class="chart__info">
            <div>{{ currentFirstTime }}</div>
            <div style="font-weight: 900;">Generals TGAM QC Results</div>
            <div>{{ currentLastTime }}</div>
          </div>
        </v-card>
        <v-card style="width: 32%; float: left">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            :isShwoLegend="false"
            @onLoadData="getTGAMExtractionData"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
        <v-divider style="width: 32%; float: left"></v-divider>
        <v-card style="width: 32%; float: left">
          <KAreaChart
            v-if="tgamExtractions.generals[0].data"
            :propDatas="tgamExtractions.generals"
            :propLabels="timeStampList"
            :isShwoLegend="false"
            @onLoadData="getTGAMExtractionData"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction LSTM: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
      </v-card>
    </v-card>
    <v-divider class="pa-2"></v-divider>
   
    <v-card class="mx-auto" style="border-top: 1px solid #001664;">
      <v-card style="width: 68%; float: left; border-right: 1px solid #001664;">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          @onLoadData="getTGAMExtractionData"
        />
        <div class="chart__info">
          <div>{{ currentFirstTime }}</div>
          <div style="font-weight: 900;">8 Bands EEG</div>
          <div>{{ currentLastTime }}</div>
        </div>
      </v-card>
      <v-card style="width: 32%; float: left">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          :isShwoLegend="false"
          @onLoadData="getTGAMExtractionData"
        />
        <!-- <v-spacer></v-spacer> -->
        <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: 213.11 - MAE: 201
      </v-card>
      <v-divider style="width: 32%; float: left"></v-divider>
      <v-card style="width: 32%; float: left">
        <KLineChart
          v-if="tgamExtractions.data8Bands[0].data"
          :propDatas="tgamExtractions.data8Bands"
          :propLabels="timeStampList"
          :isShwoLegend="false"
          @onLoadData="getTGAMExtractionData"
        />
        <!-- <v-spacer></v-spacer> -->
        <b>&nbsp;&nbsp;Prediction LSTM: </b> - RMSE: 213.11 - MAE: 201
      </v-card>
    </v-card>
  </div>
</template>

<script>
import KAreaChart from "@/components/KAreaChart.vue";
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";
import moment from "moment";

export default {
  components: {
    KAreaChart,
    KLineChart,
  },

  data() {
    return {
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      token: localStorage.getItem("token"),
      timeStampList: [],
      currentFirstTime: "",
      currentLastTime: "",
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
        .get(this.apiBaseURL + "DeviceDatas/" + this.deviceId + "/LastMin", {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        })
        .then((res) => {
          var tmp8Bands = [[], [], [], [], [], [], [], []];
          var tmpTimeStamp = [];
          var tmpGenerals = [[], [], []];

          res.data.generals.forEach((record, index) => {         
            if (index == 0) {
              this.currentFirstTime = moment(record.createdTime).format("hh:mm A - MMM DD, YYYY");
            }
            if (index == res.data.generals.length - 1) {
              this.currentLastTime = moment(record.createdTime).format("hh:mm A - MMM DD, YYYY");
            }
            tmpTimeStamp.push(
              moment(record.createdTime).format("mm:ss")
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
          console.log(this.timeStampList);
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
  },
};
</script>

<style>
.chart__info {
  display: flex;
  justify-content: space-between;
}
.chart__info div {
  padding: 0px 16px 4px 12px;
}
</style>