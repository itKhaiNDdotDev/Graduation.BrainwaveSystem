<template>
  <KLineChart />
  <KAreaChart />
  <TestGrd/>
</template>

<script>
import KLineChart from './components/KLineChart.vue'
import KAreaChart from './components/KAreaChart.vue'
import TestGrd from './components/TestGrd.vue'

export default {
  name: 'App',
  components: {
    KLineChart,
    KAreaChart,
    TestGrd
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
