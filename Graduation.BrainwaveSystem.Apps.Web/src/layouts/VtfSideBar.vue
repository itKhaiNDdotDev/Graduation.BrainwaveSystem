<template>
  <!-- class="bg-deep-purple", color="grey-darken-3", color="grey-darken-3", permanent -->
  <v-navigation-drawer
    color="#000832"
    theme="dark"
    width="256"
    v-model="drawer"
    :rail="rail"
    @click="rail = false"
    elevation="2"
  >
    <v-list-item
      height="56"
      style="background-color: #eeeeee; border-radius: 0"
      theme="light"
      nav
      elevation="2"
    >
      <div class="app__title">Brainwave System</div>
      <template v-slot:append>
        <v-btn v-if="!rail"
          variant="text"
          icon="mdi-chevron-left"
          @click.stop="rail = !rail"
          class="app__title"
        ></v-btn>
        <v-btn v-else
          variant="text"
          icon="mdi-chevron-right"
          @click.stop="rail = !rail"
          class="app__title"
        ></v-btn>
      </template>
    </v-list-item>
    <v-divider></v-divider>

    <v-list v-model:opened="open" color="transparent" density="compact" nav>
      <!-- Home -->
      <router-link to="/devices">
        <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item>
        <v-divider></v-divider>
      </router-link>

      <!-- Dah sách device -->
      <router-link
        v-for="item in listDevice"
        :key="item.id"
        :to="'/devices/' + item.id"
      >
        <v-list-group :value="item.name">
          <template v-slot:activator="{ props }">
            <v-list-item
              v-bind="props"
              prepend-icon="fa:fa fa-microchip"
              :title="item.name"
            ></v-list-item>
          </template>

          <v-list-item
            title="General"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item>
          <v-list-item
            title="TGAM Extraction"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item>
          <v-list-item
            title="Raw EEG Data"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item>
          <v-list-item
            title="Models result"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item>
        </v-list-group>
      </router-link>

      <!-- Tạo mới -->
      <!-- <v-list-item prepend-icon="mdi-add" title="Tạo mới"> -->
      <v-dialog v-model="dialog" persistent width="768">
        <template v-slot:activator="{ props }">
          <v-btn
            color="#001664"
            v-if="rail"
            v-bind="props"
            style="min-width: 32px !important"
          >
            <b style="font-size: 32px">+</b>
          </v-btn>
          <v-btn
            color="#001664"
            v-else
            v-bind="props"
            style="width: 100%; margin: 4px 0px"
          >
            <b style="font-size: 32px">+</b>&nbsp; Create New Device
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
                  <!-- type="password" -->
                  <!-- <v-text-field
                  label="Description"
                ></v-text-field> -->
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
                  <!-- <v-autocomplete
                  :items="['Skiing', 'Ice hockey', 'Soccer', 'Basketball', 'Hockey', 'Reading', 'Writing', 'Coding', 'Basejump']"
                  label="Interests"
                  multiple
                ></v-autocomplete> -->
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
      <!-- </v-list-item> -->
    </v-list>

    <!-- <div class="pa-2">
      <v-btn @click="onClickCreate" block>
        <b style="font-size: 32px">+</b>&nbsp; Create New Device
      </v-btn>
    </div> -->

    <template v-slot:append>
      <div class="pa-2">
        <v-btn color="#001664" block class="d-flex justify-center">
          <v-icon class="ma-2" icon="mdi-cog"></v-icon>
          Settings
        </v-btn>
      </div>
    </template>
  </v-navigation-drawer>

  <!-- <CreateDevicePopup class="pa-2" /> -->
  <!-- <create-device-popup></create-device-popup> -->
</template>

<script>
// import CreateDevicePopup from "@/views/CreateDevicePopup.vue";
import axios from "axios";

export default {
  comments: {
    // CreateDevicePopup,
  },

  data: () => ({
    drawer: true,
    rail: false,
    dialog: false,
    objectDevice: {},
    listDevice: [],
  }),

  methods: {
    // onClickCreate() {
    //   alert("Create");
    // },

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
        this.listDevice = response.data;
      })
      .catch((response) => {
        console.log(response);
      });
  },
};
</script>

<style scoped>
.app__title {
  font-size: 18px;
  font-weight: bolder;
  /* color: #001664; */
  color: #000422;
}

.nav__drawer {
  background-color: #000832;
}

a {
  text-decoration: none;
  color: inherit;
}
</style>