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
  
  data() {
    return {
      chartData: {
        labels: [
          "17:00",
          "17:01",
          "17:02",
          "17:03",
          "17:04",
          "17:05",
          "17:06",
          "17:07",
          "17:08",
          "17:09",
          "17:10",
          "17:11",
          "17:00",
          "17:01",
          "17:02",
          "17:03",
          "17:04",
          "17:05",
          "17:06",
          "17:07",
          "17:08",
          "17:09",
          "17:10",
          "17:11",
        ],
        datasets: [
          {
            data: [
              40, 20, 12, 22, 20, 12, 1, 20, 12, 33, 20, 40, 40, 36, 12, 23, 20,
              12, 30, 20, 12, 33, 20, 12,
            ],
            label: "Hee hee",
            backgroundColor: '#f87979',
            borderColor: "#000000",
            pointBackgroundColor: "rgba(171, 71, 188, 1)"
          },
          {
            data: [
              10, 23, 31, 12, 22, 1, 1, 12, 20, 20, 20, 11, 12, 15, 12, 20, 23,
              11, 14, 15, 30, 33, 25, 18,
            ],
            label: "Alooo",
            backgroundColor: '#343434',
            borderColor: "#FF0000"
          },
          {
            data: [
              10, 33, 1, 2, 33, 1, 1, 3, 4, 11, 15, 30, 33, 35, 37, 43, 44,
              22, 14, 15, 17, 20, 13, 1,
            ],
            label: "Test",
            backgroundColor: '#00FF00',
            borderColor: "#00FF00"
          },
        ],
      },
      chartOptions: {
        responsive: true,
      },
    };
  }
};
</script>