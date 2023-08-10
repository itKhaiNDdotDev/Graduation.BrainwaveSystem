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
            <div style="font-weight: 900">Generals TGAM QC Results</div>
            <div>{{ currentLastTime }}</div>
          </div>
        </v-card>
        <v-card style="width: 32%; float: left">
          <KLineChart
            v-if="rawChartDatas[0].data"
            :propLabels="timeStampList"
            :propDatas="rawChartDatas"
            :isShwoLegend="false"
            @onLoadData="getRawData"
          />
          <v-spacer></v-spacer>
          <b>&nbsp;&nbsp;Prediction SSA: </b> - RMSE: 213.11 - MAE: 201
        </v-card>
        <v-divider style="width: 32%; float: left"></v-divider>
        <v-card style="width: 32%; float: left">
          <KLineChart
            v-if="rawChartDatas[0].data"
            :propLabels="timeStampList"
            :propDatas="rawChartDatas"
            :isShwoLegend="false"
            @onLoadData="getRawData"
          />
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
    </v-card>
  </div>
</template>

<script>
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";
import moment from "moment";

export default {
  components: {
    KLineChart,
  },

  data() {
    return {
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      token: localStorage.getItem("token"),
      timeStampList: [],
      rawChartDatas: [{}],
      fftFrequencyAxis: [],
      rawFFTChartDatas: [{}],
      currentFirstTime: "",
      currentLastTime: "",
      awakeState: {
        FastTree: "",
        SVM: ""
      }
    };
  },
  props: ["deviceId"],

  methods: {
    getRawData() {
      axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/Last15Secs", {
          headers: { Authorization: "Bearer " + this.token },
        })
        .then((res) => {
          var tmpValues = [];
          var tmpTimeStamp = [];
          res.data.forEach((record, index) => {
            if (index == 0) {
              this.currentFirstTime = moment.utc(record.createdTime).local().format("hh:mm A - MMM DD, YYYY");
            }
            if (index == res.data.length - 1) {
              this.currentLastTime = moment.utc(record.createdTime).local().format("hh:mm A - MMM DD, YYYY");
            }
            tmpTimeStamp.push(
              moment.utc(record.createdTime).local().format("mm:ss")
            );
            //this.timeStampList.fill("", this.timeStampList.length, this.timeStampList.length + record.values.length - 1);
            tmpValues.push(record.value);
          });
          this.timeStampList = tmpTimeStamp;
          this.rawChartDatas[0].data = tmpValues;
          this.rawChartDatas[0].lblName = "Raw EEG";
          this.rawChartDatas[0].bgColor = "darkblue";

          console.log("On GetRawEEGData");
        })
        .catch((err) => {
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
          console.log(res);
          console.log(this.rawFFTChartDatas);
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
    }
  },

  created() {
    this.getRawData();
    this.getFFTData();
    this.getFastTreeAwakeState();
    // console.log("RawPar Created");
  },
};
</script>

<style lang="">
</style>