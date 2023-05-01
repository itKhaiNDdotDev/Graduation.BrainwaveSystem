<template>
  <Line id="my-chart-id" :options="chartOptions" :data="chartData" />
</template>

<script>
import { Line } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  LineElement,
  PointElement,
  CategoryScale,
  LinearScale,
} from "chart.js";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  LineElement,
  PointElement,
  CategoryScale,
  LinearScale
);

export default {
  name: "LineChart",
  components: { Line },

  props: {
    propLabels: Array,
    propDatas: Array
  },
  
  data() {
    return {
      chartData: {
        labels: this.propLabels,
        datasets: [
          {
            data: this.propDatas,
            label: "Hee hee",
            backgroundColor: 'transparent',
            pointBackgroundColor: 'transparent',
            borderColor: "#000000",
            borderWidth: 1,
            //pointBackgroundColor: "rgba(171, 71, 188, 1)",
            tension: 0.5,
            pointBorderWidth: 0
          },
          // {
          //   data: [
          //     10, 23, 31, 12, 22, 1, 1, 12, 20, 20, 20, 11, 12, 15, 12, 20, 23,
          //     11, 14, 15, 30, 33, 25, 18,
          //   ],
          //   label: "Alooo",
          //   backgroundColor: '#343434',
          //   borderColor: "#FF0000"
          // },
          // {
          //   data: [
          //     10, 33, 1, 2, 33, 1, 1, 3, 4, 11, 15, 30, 33, 35, 37, 43, 44,
          //     22, 14, 15, 17, 20, 13, 1,
          //   ],
          //   label: "Test",
          //   backgroundColor: '#00FF00',
          //   borderColor: "#00FF00"
          // },
        ],
      },
      chartOptions: {
        responsive: true,
      },
    };
  }
};
</script>

<style>
</style>