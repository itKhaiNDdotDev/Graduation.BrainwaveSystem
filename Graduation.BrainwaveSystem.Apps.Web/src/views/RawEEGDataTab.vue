<template>
  <div>
    <KLineChart
      :key="lineChartKey"
      :propLabels="timeStampList"
      :propDatas="rawValues"
      @onLoadData="getRawData"
    />
    <div>{{ currentTime }}</div>
    <v-btn @click="onClickRefresh">Refresh</v-btn>
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
      timeStampList: [],
      rawValues: [],
      currentTime: "",
    };
  },

  methods: {
    getRawData() {
      axios
        .get(
          "https://localhost:44321/api/DataRawEEGs/bcb6bd84-8247-4cce-acb4-48487b9015bb/Last15Secs"
        )
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
          this.rawValues = tmpValues;
          console.log(this.timeStampList);
          console.log(this.rawValues);
          this.lineChartKey++;
        })
        .catch((err) => {
          console.log(err);
        });
    },

    created() {
      //this.getRawData();
    },
  },
};
</script>

<style lang="">
</style>