<template>
  <v-sheet class="pa-1 d-flex align-content-start flex-wrap">
    <!-- Danh sách -->
    <v-btn
      v-for="item in ListDevice"
      :key="item.id"
      class="btn ma-3 d-flex justify-center align-center flex-nowrap"
      elevation="10"
      height="180"
      width="190"
      theme="dark"
      color="#001664"
      @click="onClickDevice(item.id)"
    >
      <v-icon class="ma-2" icon="fa:fa fa-microchip"></v-icon>
      {{ item.name }}
    </v-btn>

    <!-- Thêm mới -->
    <v-dialog v-model="dialog" persistent width="768">
      <template v-slot:activator="{ props }">
        <v-btn
          class="ma-3"
          elevation="10"
          height="180"
          width="180"
          theme="dark"
          color="#001664"
          v-bind="props"
        >
          <v-sheet
            class="d-flex justify-center flex-column align-center"
            width="180"
            color="transparent"
            height="180"
          >
            <div>
              <div style="font-size: 48px; font-weight: bolder">+</div>
              <div
                class="d-flex justify-center flex-wrap"
                style="width: 100%; font-weight: bold"
              >
                Create New Device
              </div>
            </div>
          </v-sheet>
        </v-btn>
      </template>
      <v-card theme="light">
        <v-card-title>
          <span class="text-h5">Device Information</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  label="Name *"
                  required
                  v-model="objectDevice.name"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-textarea
                  label="Description"
                  rows="4"
                  v-model="objectDevice.description"
                ></v-textarea>
              </v-col>
              <v-col cols="12" sm="6">
                <v-select
                  :items="['Active', 'Unactive']"
                  label="Status"
                ></v-select>
              </v-col>
              <v-col cols="12" sm="6">
                <small
                  style="height: 80%"
                  class="d-flex align-center justify-center text-center w-100"
                  >* Indicates required field</small
                >
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue-darken-1" variant="text" @click="dialog = false">
            Close
          </v-btn>
          <v-btn color="blue-darken-1" variant="text" @click="onClickSave">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-sheet>
</template>

<script>
import axios from "axios";
import router from "@/routers";

export default {
  name: "ListDevice",
  data: () => ({
    ListDevice: [],
    dialog: false,
    objectDevice: {},
  }),

  methods: {
    onClickDevice(id) {
      router.push("/devices/" + id);
    },

    onClickSave() {
      this.objectDevice.isActive = true;
      axios
        .post("https://localhost:44321/api/Devices", this.objectDevice)
        .then((response) => {
          console.log(response);
        })
        .catch((response) => {
          console.log(response);
        });
      this.dialog = false;
      this.$emit("onRefresh");
    },
  },

  created() {
    axios
      .get("https://localhost:44321/api/Devices")
      .then((response) => {
        console.log(response);
        this.ListDevice = response.data;
      })
      .catch((response) => {
        console.log(response);
      });
  },
};
</script>

<style scoped>
.btn {
  text-transform: none !important;
  font-weight: 500;
  font-size: 18px;
  padding: 20px;
  width: 100%;
  box-sizing: border-box;
  word-wrap: break-word;
}
</style>