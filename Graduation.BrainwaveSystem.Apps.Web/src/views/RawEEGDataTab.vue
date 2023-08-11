<template>
  <div>
    <v-card class="mx-auto">
      <v-card class="mx-auto" style="border-top: 1px solid #001664">
        <v-card
          style="width: 68%; float: left; border-right: 1px solid #001664"
        >
          <KLineChart
            v-if="rawChartDatas[0].data"
            :propLabels="timeStampList"
            :propDatas="rawChartDatas"
            @onLoadData="getRawData"
          />
          <div class="chart__info">
            <div>{{ currentFirstTime }}</div>
            <div style="font-weight: 900">EEG raw data (Fs: 512Hz)</div>
            <div>{{ currentLastTime }}</div>
          </div>
        </v-card>
        <v-card style="width: 32%; float: left">
          <KLineChart
            v-if="rawPreictChartDatas[0].data"
            :propLabels="timeStampPredictList"
            :propDatas="rawPreictChartDatas"
            :isShwoLegend="false"
            :fixScale="{minY: -600, maxY: 600}"
            @onLoadData="getSSAPredict"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: {{rawPredictEvaluation.RMSE}} - MAE: {{rawPredictEvaluation.MAE}}
        </v-card>
        <v-divider style="width: 32%; float: left"></v-divider>
        <v-card style="width: 32%; float: left">
          <!-- <KLineChart/> -->
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction LSTM: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
      </v-card>
    </v-card>

    <v-divider></v-divider>
    <v-card class="d-flex justify-space-around py-4">
      <div>
        State SVM - DeepSleetNet: <b>{{awakeState.SVM}} - {{"NREM S1"}}</b>
        <div>Train Score: 97.04%</div>
        <div>Accuracy: 94.13%</div>
      </div>
      <v-btn text="Refresh" @click="onClickReloadModelResults"></v-btn>
      <div>
        State FastTree - DeepSleetNet: <b>{{awakeState.FastTree}} - {{}}</b>
        <div>Train Score: 99.51%</div>
        <div>Accuracy: 97.06%</div>
      </div>
    </v-card>

    <v-card class="mx-auto" style="border-top: 1px solid #001664">
      <KLineChart
        v-if="rawFFTChartDatas[0].data"
        :propLabels="fftFrequencyAxis"
        :propDatas="rawFFTChartDatas"
        @onLoadData="getFFTData"
      />
      <div class="chart__info">
            <div></div>
            <div style="font-weight: 900">FFT for raw data with window 15 seconds</div>
            <div></div>
          </div>
    </v-card>

    <VtfLoading v-if="isShowLoading" />
  </div>
</template>

<script>
import KLineChart from "@/components/KLineChart.vue";
import VtfLoading from "@/components/VtfLoading.vue";
import axios from "axios";
import moment from "moment";

export default {
  components: {
    KLineChart,
    VtfLoading,
  },

  data() {
    return {
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      token: localStorage.getItem("token"),
      timeStampList: [],
      rawChartDatas: [{}],
      rawPreictChartDatas: [{}, {}, {}],
      timeStampPredictList: [],
      rawPredictEvaluation: {},
      fftFrequencyAxis: [],
      rawFFTChartDatas: [{}],
      currentFirstTime: "",
      currentLastTime: "",
      awakeState: {
        FastTree: "",
        SVM: ""
      },
      isShowLoading: true,
    };
  },
  props: ["deviceId"],

  methods: {
    getRawData() {
      axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/Last10Secs", {
          headers: { Authorization: "Bearer " + this.token },
        })
        .then((res) => {
          var tmpTimeStamp = [];
          res.data.recivedTimes.forEach((record, index) => {
            if (index == 0) {
              this.currentFirstTime = moment.utc(record).local().format("hh:mm A - MMM DD, YYYY");
            }
            if (index == res.data.recivedTimes.length - 1) {
              this.currentLastTime = moment.utc(record).local().format("hh:mm A - MMM DD, YYYY");
            }
            tmpTimeStamp.push(moment.utc(record).local().format("mm:ss"));
          });
          this.timeStampList = tmpTimeStamp;
          this.rawChartDatas[0].data = res.data.values;
          this.rawChartDatas[0].lblName = "Raw EEG";
          this.rawChartDatas[0].bgColor = "darkblue";
        })
        .catch((err) => {
          this.isShowLoading = false;
          console.log(err);
        });
    },

    getFFTData() {
      axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/FFTFix", {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        })
        .then((res) => {
          this.fftFrequencyAxis = res.data.indexs;//frequencyAxis;
          this.rawFFTChartDatas[0].data = res.data.values;//amplitudeSpectrum;
          this.rawFFTChartDatas[0].lblName = "Amplitude Spectrum";
          this.rawFFTChartDatas[0].bgColor = "darkblue";
        })
        .catch((err) => {
          console.log(err);
        });
    },

    getFastTreeAwakeState() {
      axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/fasttree", {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        })
        .then((res) => {
          this.awakeState.FastTree = res.data.stateLabel
        })
        .catch((err) => {
          console.log(err);
        });
    },

    onClickReloadModelResults() {
      this.getFastTreeAwakeState();
    },

    async getSSAPredict() {
      await axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/SSAPredict", {
          headers: { Authorization: "Bearer " + this.token },
        })
        .then((res) => {
          var tmpTimeStampPredict = [];
          res.data.predictTimes.forEach((record) => {
            tmpTimeStampPredict.push(moment.utc(record).local().format("mm:ss"));
          });
          this.timeStampPredictList = tmpTimeStampPredict;

          this.rawPreictChartDatas[0].data = res.data.forecastedValues;
          this.rawPreictChartDatas[0].lblName = "Raw EEG";
          this.rawPreictChartDatas[0].bgColor = "darkblue";

          this.rawPreictChartDatas[1].data = res.data.lowerBoundValues;
          this.rawPreictChartDatas[1].lblName = "Lower bound";
          this.rawPreictChartDatas[1].bgColor = "black";
          this.rawPreictChartDatas[1].lineThickness = 0.3;

          this.rawPreictChartDatas[2].data = res.data.upperBoundValues;
          this.rawPreictChartDatas[2].lblName = "Upper bound";
          this.rawPreictChartDatas[2].bgColor = "black";
          this.rawPreictChartDatas[2].lineThickness = 0.3;

          this.rawPredictEvaluation = {
            RMSE: res.data.rmse.toFixed(2),
            MAE: res.data.mae.toFixed(2)
          };
           this.isShowLoading = false;
        })
        .catch((err) => {
          this.isShowLoading = false;
          console.log(err);
        });
    },
  },

  created() {
    this.getRawData();
    this.getFFTData();
    this.getFastTreeAwakeState();
    this.getSSAPredict();
    // console.log("RawPar Created");
  },
};
</script>

<style lang="">
</style>