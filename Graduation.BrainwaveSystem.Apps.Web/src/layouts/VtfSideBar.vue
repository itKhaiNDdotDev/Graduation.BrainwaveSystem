<template>
  <v-navigation-drawer
    color="grey-darken-3"
    class="bg-deep-purple"
    theme="dark"
    permanent
    width="256"
    v-model="drawer"
    :rail="rail"
    @click="rail = false"
  >
    <v-list-item title="Brainwave System" nav>
      <template v-slot:append>
        <v-btn
          variant="text"
          icon="mdi-chevron-left"
          @click.stop="rail = !rail"
        ></v-btn>
      </template>
    </v-list-item>

    <v-divider></v-divider>

    <v-list v-model:opened="open" color="transparent" density="compact" nav>
      <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item>

      <v-list-group v-for="item in listDevice" :key="item.id" :value=item.name>
        <template v-slot:activator="{ props }">
          <v-list-item
            v-bind="props"
            prepend-icon="mdi-account-circle"
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
    </v-list>

    <div class="pa-2">
      <v-btn @click="onClickCreate" block>
        <b style="font-size: 32px">+</b>&nbsp; Create New Device
      </v-btn>
    </div>

    <template v-slot:append>
      <div class="pa-2">
        <v-btn block> Settings </v-btn>
      </div>
    </template>
  </v-navigation-drawer>

  <CreateDevicePopup class="pa-2" />
  <create-device-popup></create-device-popup>

  <v-row
    justify="center"
    style="z-index: 906; position: absolute; top: 72px; left: 30px"
  >
    <!-- <div class="pa-2" style="z-index: 999;"> -->
    <v-dialog v-model="dialog" persistent width="768">
      <template v-slot:activator="{ props }">
        <v-btn color="primary" v-bind="props">
          <b style="font-size: 32px">+</b>&nbsp; Create New Device
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="text-h5">Device Information</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field label="Name *" required v-model="objectDevice.name"></v-text-field>
              </v-col>
              <v-col cols="12">
                <!-- type="password" -->
                <!-- <v-text-field
                  label="Description"
                ></v-text-field> -->
                <v-textarea label="Description" rows="4" v-model="objectDevice.description"></v-textarea>
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
    <!-- </div> -->
  </v-row>
</template>

<script>
import CreateDevicePopup from "@/views/CreateDevicePopup.vue";
import axios from "axios";

export default {
  comments: {
    CreateDevicePopup,
  },

  data: () => ({
    open: ["Users"],
    admins: [
      ["Management", "mdi-account-multiple-outline"],
      ["Settings", "mdi-cog-outline"],
    ],
    cruds: [
      ["Create", "mdi-plus-outline"],
      ["Read", "mdi-file-outline"],
      ["Update", "mdi-update"],
      ["Delete", "mdi-delete"],
    ],
    drawer: true,
    rail: false,
    dialog: false,
    objectDevice: {},
    listDevice: []
  }),

  methods: {
    onClickCreate() {
      alert("Create");
    },

    onClickSave() {
      this.objectDevice.isActive = true;
      axios.post("https://localhost:44321/api/Devices", this.objectDevice)
          .then((response) => {
            console.log(response);
            
          })
          .catch((response) => {
            console.log(response);
          });
      this.dialog = false;
    }
  },

  created() {
    axios.get("https://localhost:44321/api/Devices")
          .then((response) => {
            console.log(response);
            this.listDevice = response.data;
          })
          .catch((response) => {
            console.log(response);
          });
  }
};
</script>