<template>
  <Line :options="chartOptions" :data="chartData" />
  <v-btn @click="onClickRefresh">Refresh</v-btn>
</template>

<script>
import { Line } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  Filler,
  PointElement,
  LineElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
ChartJS.register(
  Title,
  Tooltip,
  Legend,
  Filler,
  PointElement,
  LineElement,
  CategoryScale,
  LinearScale
);

export default {
  name: "KAreaChart",
  components: { Line },

  props: {
    propLabels: Array,
    propDatas: Array,
  },

  data() {
    return {
      chartData: {
        labels: this.propLabels,
        datasets: [
          {
            label: "Data One",
            borderColor: "#FC2525",
            pointBackgroundColor: "white",
            borderWidth: 1,
            pointBorderColor: "white",
            backgroundColor: (ctx) => {
              // định màu area
              const canvas = ctx.chart.ctx;
              const gradient = canvas.createLinearGradient(0, 0, 0, 160);

              gradient.addColorStop(0, "green");
              gradient.addColorStop(0.5, "cyan");
              gradient.addColorStop(1, "rgba(0, 231, 255, 0.3)");

              return gradient;
            },
            tension: 0.25, // bo tròn line
            fill: true, // dùng area fill
            radius: 0,
            data: this.propDatas[0],
          },
          {
            label: "Data Two",
            borderColor: "#05CBE1",
            pointBackgroundColor: "blue",
            pointBorderColor: "blue",
            backgroundColor: "blue",
            borderWidth: 1,
            //backgroundColor: this.gradient2,
            data: this.propDatas[1],
            fill: true,
          },
          {
            label: "One",
            data: [40, 20, 12, 3, 4, 5, 1],
            fill: true,
            tension: 1,
            backgroundColor: "#FF002244",
          },
        ],
      },

      chartOptions: { responsive: true, maintainAspectRatio: true },
    };
  },

  methods: {
    onClickRefresh() {
      // emit to get API data
      this.$emit("onLoadData");
      this.updateChartData();
    },

    updateChartData() {
      var tmpDatasets = [];
      this.propDatas.forEach((propData) => {
        var tmp = {
          label: propData.lblName,
          backgroundColor: propData.bgColor + "44",
          borderWidth: 1,
          fill: true,
          tension: 0.5,
          data: propData.data,
          borderColor: propData.bgColor,
          pointRadius: 0, // loại bỏ hiển thị điểm
        };
        tmpDatasets.push(tmp);
      });
      this.chartData = {
        ...this.chartData,
        datasets: tmpDatasets,
      };
    },
  },

  created() {
    //this.$emit("onLoadData");
    console.log("Before Mount");
  },

  mounted() {
    // this.gradient = this.$refs.canvas.getContext("2d").createLinearGradient(0, 0, 0, 450);
    // this.gradient2 = this.$refs.canvas.getContext("2d").createLinearGradient(0, 0, 0, 450);

    // this.gradient.addColorStop(0, "rgba(255, 0,0, 0.5)");
    // this.gradient.addColorStop(0.5, "rgba(255, 0, 0, 0.25)");
    // this.gradient.addColorStop(1, "rgba(255, 0, 0, 0)");

    // this.gradient2.addColorStop(0, "rgba(0, 231, 255, 0.9)");
    // this.gradient2.addColorStop(0.5, "rgba(0, 231, 255, 0.25)");
    // this.gradient2.addColorStop(1, "rgba(0, 231, 255, 0)");
    var tmpDatasets = [];
    this.propDatas.forEach((propData) => {
      var tmp = {
        label: propData.lblName,
        backgroundColor: propData.bgColor + "88",
        borderWidth: 1,
        fill: true,
        tension: 0.5,
        data: propData.data,
        borderColor: propData.bgColor,
        pointRadius: 0, // loại bỏ hiển thị điểm
        pointWidth: 4,
      };
      tmpDatasets.push(tmp);
    });
    this.chartData = {
      ...this.chartData,
      datasets: tmpDatasets,
    };
    console.log("Mounted");
  },
};
</script>

<style scoped>
.chart-container .chartjs-render-monitor {
  background-color: rgba(75, 192, 192, 0.2);
}
</style>