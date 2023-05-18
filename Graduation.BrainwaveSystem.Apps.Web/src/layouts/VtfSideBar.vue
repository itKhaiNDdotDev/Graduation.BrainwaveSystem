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
    <v-list-item
      title="Brainwave System"
      nav
    >
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

      <v-list-group value="KhaiND Test">
        <template v-slot:activator="{ props }">
          <v-list-item
            v-bind="props"
            prepend-icon="mdi-account-circle"
            title="KhaiND Test"
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
        <!-- <v-list-group value="General">
          <template v-slot:activator="{ props }">
            <v-list-item v-bind="props" title="Admin"></v-list-item>
          </template>

          <v-list-item
            v-for="([title, icon], i) in admins"
            :key="i"
            :title="title"
            :prepend-icon="icon"
            :value="title"
          ></v-list-item>
        </v-list-group>

        <v-list-group value="TGAM Extraction">
          <template v-slot:activator="{ props }">
            <v-list-item v-bind="props" title="Actions"></v-list-item>
          </template>

          <v-list-item
            v-for="([title, icon], i) in cruds"
            :key="i"
            :value="title"
            :title="title"
            :prepend-icon="icon"
          ></v-list-item>
        </v-list-group> -->
      </v-list-group>
    </v-list>

    <template v-slot:append>
      <div class="pa-2">
        <v-btn block> Settings </v-btn>
      </div>
    </template>
  </v-navigation-drawer>
</template>

<script>
export default {
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
  }),
};
</script>