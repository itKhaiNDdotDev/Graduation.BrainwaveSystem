<template>
  <Line :options="chartOptions" :data="chartData" />
</template>

<script>
import { Line } from "vue-chartjs";

export default {
  components: {
    Line
  },

  props: {
    propLabels: Array,
    propDatas: Array
  },

  data() {
    return {
      gradient: null,
      gradient2: null,

      chartData: {
        labels: this.propLabels,
        datasets: [
          {
            label: "Data One",
            borderColor: "#FC2525",
            pointBackgroundColor: "white",
            borderWidth: 1,
            pointBorderColor: "white",
            //backgroundColor: this.gradient,
            backgroundColor: (ctx) => { // định màu area
              const canvas = ctx.chart.ctx;
              const gradient = canvas.createLinearGradient(0,0,0,160);

              gradient.addColorStop(0, 'green');
              gradient.addColorStop(.5, 'cyan');
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
            fill: true
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

      chartOptions: { responsive: true, maintainAspectRatio: true }
    };
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
  },
};
</script>

<style scoped>
.chart-container .chartjs-render-monitor {
  background-color: rgba(75, 192, 192, 0.2);
}
</style>