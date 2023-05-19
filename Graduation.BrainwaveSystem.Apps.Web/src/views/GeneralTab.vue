<template>
  <div class="info__wrapper">
    <div class="info__item">
      <span>Name:</span>
      <span>
        <b>{{device.name}}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Description:</span>
      <span>
        <b>{{device.description}}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Status:</span>
      <span>
        <b>{{device.status}}</b>
      </span>
    </div>
    <div class="info__item">
      <span>Created Date::</span>
      <span>
        <b>{{device.createdTime}}</b>
      </span>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      device: {},
    };
  },

  created() {
    axios
      .get("https://localhost:44321/api/Devices/5b85104d-a8b4-4059-a3a0-f9991287b380")
      .then((res) => {
        this.device = res.data;
        console.log(res);
        if(res.data.isActive) {
            this.device.status = "Active";
        }
        else {
            this.device.status = "Unactive";
        }
      })
      .catch((err) => {
        console.log(err);
      });
  },
};
</script>

<style scoped>
.info__wrapper {
  margin: 32px;
}

.info__item {
  padding-bottom: 8px;
  display: grid;
  grid-template-columns: 128px auto;
}

.info__item span {
}
</style>