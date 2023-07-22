<template>
  <div>
    <KLineChart
      v-if="rawChartDatas[0].data"
      :propLabels="timeStampList"
      :propDatas="rawChartDatas"
      @onLoadData="getRawData"
    />
    <div>{{ currentTime }}</div>
    <KLineChart
      v-if="rawFFTChartDatas[0].data"
      :propLabels="fftFrequencyAxis"
      :propDatas="rawFFTChartDatas"
      @onLoadData="getFFTData"
    />
  </div>
</template>

<script>
import KLineChart from "@/components/KLineChart.vue";
import axios from "axios";

export default {
  components: {
    KLineChart,
  },

  data() {
    return {
      apiBaseURL: process.env.VUE_APP_API_BASE_URL,
      timeStampList: [],
      rawChartDatas: [{}],
      fftFrequencyAxis: [],
      rawFFTChartDatas: [{}],
      currentTime: "",
    };
  },
  props: ["deviceId"],

  methods: {
    getRawData() {
      axios
        .get(this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/Last15Secs")
        .then((res) => {
          var tmpValues = [];
          var tmpTimeStamp = [];
          res.data.forEach((record, index) => {
            const dateTime = new Date(record.recivedTime);
            const minutes = dateTime.getMinutes();
            const seconds = dateTime.getSeconds();
            if (index == res.data.length - 1) {
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
        .get(
          this.apiBaseURL + "DataRawEEGs/" + this.deviceId + "/FFT"
        )
        .then((res) => {
          this.fftFrequencyAxis = res.data.frequencyAxis;
          this.rawFFTChartDatas[0].data = res.data.amplitudeSpectrum;
          this.rawFFTChartDatas[0].lblName = "Amplitude Spectrum";
          this.rawFFTChartDatas[0].bgColor = "darkblue";
          console.log(res);
          console.log(this.rawFFTChartDatas);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },

  created() {
    this.getRawData();
    this.getFFTData();
    // console.log("RawPar Created");
  },
};
</script>

<style lang="">
</style>