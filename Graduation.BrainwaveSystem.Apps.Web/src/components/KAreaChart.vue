<template>
  <Line :options="chartOptions" :data="chartData" />
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
    labelProps: Array,
    dataProps: Array
  },

  data() {
    return {
      chartData: {
        labels: this.labelProps,//["January", "February", "March", "April", "May", "Jun", "July"],
        datasets: [
            {
                label: "One",
                data: this.dataProps[0],//[40, 20, 12, 3, 4, 5, 1] ,
                fill: true,
                tension: 0.25,
                backgroundColor: "#FF002244"
            },
            {
                label: "Two",
                data: this.dataProps[1],//[15, 22, 12, 13, 24, 5, 41] ,
                fill: true,
                tension: 0.25,
                backgroundColor: "#00FF8844"
            }
        ],
      },
      chartOptions: {
        responsive: true,
      },
    };
  },

  onUpdated() {
    console.log(this.chartData.labels);
  },
};
</script>