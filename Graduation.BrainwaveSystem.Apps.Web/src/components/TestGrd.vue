<template>
  <Line
    :chartData="chartData"
    :chartOptions="chartOptions"
    :chartId="chartId"
    :width="width"
    :height="height"
    :cssClasses="cssClasses"
    :styles="styles"
    :plugins="plugins"
    
  />
</template>

<script>
import { Line } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  //Plugin,
  Filler,
  //BorderRadius
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, LineElement, LinearScale, CategoryScale, PointElement, Filler)

export default {
  name: 'TestChart',
  components: { Line },

  props: {
    chartId: {
      type: String,
      default: 'line-chart'
    },
    width: {
      type: Number,
      default: 400
    },
    height: {
      type: Number,
      default: 160
    },
    cssClasses: {
      default: '',
      type: String
    },
    styles: {
      type: Object,
      default: () => {}
    },
    plugins: {
      type: Array,
      default: () => []
    }
  },

  data() {
    return {
      chartData: {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        datasets: [
          {
            label: 'Data One',
            borderColor: '#FC2525',
            pointBackgroundColor: 'white',
            borderWidth: 1,
            radius: 0,
            fill: true,
            pointBorderColor: 'white',
            backgroundColor: (ctx) => {
              const canvas = ctx.chart.ctx;
              const gradient = canvas.createLinearGradient(0,0,0,160);

              gradient.addColorStop(0, 'green');
              gradient.addColorStop(.5, 'cyan');
              gradient.addColorStop(1, 'green');

              return gradient;
            },
            tension: 0.25,
            data: [40, 39, 10, 40, 39, 80, 40]
          },{
            label: 'Data Two',
            borderColor: '#05CBE1',
            pointBackgroundColor: 'white',
            pointBorderColor: 'white',
            borderWidth: 1,
            radius: 0,
            fill: true,
            backgroundColor: (ctx) => {
              const canvas = ctx.chart.ctx;
              const gradient = canvas.createLinearGradient(0,0,0,160);

              gradient.addColorStop(0, 'green');
              gradient.addColorStop(.5, 'cyan');
              gradient.addColorStop(1, 'green');

              return gradient;
            },
            tension: 0.25,
            data: [60, 55, 32, 10, 2, 12, 53]
          }
        ]
      },
      chartOptions: {
        responsive: true,
        plugins: {
          legend: {
            display: false
          }
        }
      }
    }
  },
}
</script>