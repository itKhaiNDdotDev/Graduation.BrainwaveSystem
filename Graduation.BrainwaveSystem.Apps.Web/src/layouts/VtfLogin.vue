<template>
  <v-main>
    <v-container class="fill-height" fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4" lg="4">
          <v-card elevation="0" style="text-align: center">
            <img
              src="../assets/logo.png"
              style="width: 200px; height: 100px; object-fit: contain"
            />
            <!-- <div class="login__logo"></div> -->
            <v-card-text>
              <v-form>
                <v-text-field
                  label="Enter your Username"
                  v-model="objectLogin.userName"
                  theme="dark"
                  color="#001664"
                ></v-text-field>
                <v-text-field
                  label="Enter your Password"
                  v-model="objectLogin.password"
                  type="password"
                  theme="dark"
                  color="#000832"
                ></v-text-field>
                <v-btn
                  class="rounded-0"
                  style="color: white; background-color: #000832"
                  x-large
                  block
                  dark
                  @click="onClickLogin()"
                  >Login</v-btn
                >
                <v-card-actions style="color: 000832">
                  <v-checkbox
                    v-model:focused="objectLogin.rememberMe"
                    color="#000000"
                    label="Remember Me"
                  ></v-checkbox>
                  <v-spacer></v-spacer>
                  <div style="padding-bottom: 15px">
                    No Account?
                    <v-dialog
                      theme="light"
                      v-model="openRegister"
                      persistent
                      width="768"
                    >
                      <template v-slot:activator="{ props }">
                        <v-btn v-if="rail" v-bind="props" V->
                          <b style="font-size: 32px">+</b>
                        </v-btn>
                        <v-btn v-else v-bind="props"> Register </v-btn>
                      </template>
                      <v-card theme="light">
                        <v-card-title>
                          <span
                            style="color: #000832; font-weight: bold"
                            class="text-h5"
                            >REGISTER USER</span
                          >
                        </v-card-title>
                        <v-card-text>
                          <v-container>
                            <v-row>
                              <v-col cols="12" sm="6">
                                <v-text-field
                                  color="#000832"
                                  label="Username *"
                                  required
                                  v-model="objectRegister.userName"
                                ></v-text-field>
                              </v-col>
                              <v-col cols="12" sm="6">
                                <v-text-field
                                  color="#000832"
                                  label="Email *"
                                  required
                                  v-model="objectRegister.email"
                                  placeholder="ex: abc@gmail.com"
                                  type="email"
                                ></v-text-field>
                              </v-col>
                              <v-col cols="12" sm="6">
                                <v-text-field
                                  color="#000832"
                                  label="Password *"
                                  required
                                  v-model="objectRegister.passwordHash"
                                  type="password"
                                >
                                </v-text-field>
                              </v-col>
                              <v-col cols="12" sm="6">
                                <v-text-field
                                  color="#000832"
                                  label="Comfirm Password *"
                                  required
                                  v-model="objectRegister.passwordSalt"
                                  type="password"
                                >
                                </v-text-field>
                              </v-col>
                              <v-col cols="12">
                                <v-text-field
                                  color="#000832"
                                  label="Full Name *"
                                  required
                                  v-model="objectRegister.name"
                                ></v-text-field>
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-card-text>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn
                            color="#000832"
                            variant="text"
                            @click="openRegister = false"
                          >
                            Close
                          </v-btn>
                          <v-btn
                            color="white"
                            style="background-color: #001664"
                            variant="text"
                            @click="onClickRegister"
                          >
                            Save
                          </v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                  </div>
                </v-card-actions>
              </v-form>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>

  <v-dialog theme="light" v-model="resultCard">
    <v-sheet
      elevation="12"
      max-width="400"
      rounded="lg"
      width="100%"
      class="pa-4 text-center mx-auto"
    >
      <div v-if="resultInfo.status == 0">
        <v-icon
          class="mb-5"
          color="error"
          icon="mdi-alert-circle"
          size="112"
        ></v-icon>
        <h2 class="text-h5 mb-6">Fail!</h2>
      </div>
      <div v-else-if="resultInfo.status == 1">
        <v-icon
          class="mb-5"
          color="success"
          icon="mdi-check-circle"
          size="112"
        ></v-icon>
        <h2 class="text-h5 mb-6">Success!</h2>
      </div>

      {{ resultInfo.message }}
      <br />
      <p
        class="mb-4 text-medium-emphasis text-body-2"
        v-if="resultInfo.linkHref"
      >
        {{ resultInfo.linkPreNote }}
        <router-link
          :to="resultInfo.linkHref"
          class="text-decoration-none text-info"
          >{{ resultInfo.linkTitle ? resultInfo.linkTitle : "Go to link." }}.
        </router-link>
      </p>
    </v-sheet>
  </v-dialog>

  <VtfLoading v-if="isShowLoading" />
</template>

<script>
import axios from "axios";
import VtfLoading from "@/components/VtfLoading.vue";
export default {
  props: ["appName"],
  components: {
    VtfLoading,
  },

  data: () => ({
    isShowLoading: false,
    apiBaseURL: process.env.VUE_APP_API_BASE_URL,
    openRegister: false,
    hideNonLogin: true,
    rail: false,
    token: localStorage.getItem("token"),
    objectLogin: {
      userName: "",
      password: "",
      returnUrl: "",
      rememberMe: false,
    },
    objectRegister: {
      isDeleted: false,
      createdTime: "2023-07-26T16:34:01.067Z",
      createdBy: "user",
      lastModifiedTime: "2023-07-26T16:34:01.067Z",
      lastModifiedBy: "user",
      id: "80189fbe-99e8-45d9-cdb1-08db90531402",
      name: "",
      userName: "",
      passwordHash: "",
      passwordSalt: "",
      photoFileName: "anonymous.pns",
      role: 2,
      email: "",
    },
    fullname: "",
    resultCard: false,
    resultInfo: {
      status: null, // 0: Fail, 1: Success, 2: Warning, 3: Note
      message: "",
      linkPreNote: "",
      linkHref: "",
      linkTitle: "",
    },
  }),

  methods: {
    async onClickLogin() {
      this.isShowLoading = true
      try {
        
        await axios
          .post(this.apiBaseURL + "Users/LoginUser", this.objectLogin)
          .then((response) => {
            if (response.data.value == undefined) {
              this.setAlert(0, "Login failed!!", 2000);
            } else {
              localStorage.setItem("token", response.data.value);
              this.setAlert(1, "Login successful!!", 2000);
              window.location.reload();
            }
          }).catch((err) => {
            console.log(err);
            this.setAlert(0, "There is an error on the server! Please contact admin for assistance.", 2000);
          });
      } catch (ex) {
        console.log(ex);
        this.isShowLoading = false
      }
      this.isShowLoading = false
    },

    async onClickRegister() {
      this.isShowLoading = true
      try {
        if (
          this.objectRegister.passwordHash !== this.objectRegister.passwordSalt
        )
          this.setAlert(0, "Password does not match", 2000);
        else
          await axios
            .post(this.apiBaseURL + "Users/Register", this.objectRegister)
            .then((response) => {
              console.log(response.data);
              this.openRegister = false;
              this.setAlert(1, "Registered Sucessfull!!", 2000);
            })
            .catch((error) => {
              console.log(error.response);
              if (error.response.data.status == 400)
                this.setAlert(
                  0,
                  "Fill in mandatory and correct structure!!",
                  2000
                );
              else if (error.response.status == 400)
                this.setAlert(0, error.response.data, 2000);
                this.isShowLoading = false
            });
      } catch (ex) {
        console.log(ex);
      }
      this.isShowLoading = false
    },

    setAlert(stt, mess, time) {
      (this.resultInfo = {
        status: stt, // 0: Fail, 1: Success, 2: Warning, 3: Note
        message: mess,
        linkPreNote: "",
        linkHref: "",
        linkTitle: "",
      }),
        (this.resultCard = true);
      setTimeout(() => {
        this.resultCard = false;
      }, time);
    },
  },
};
</script>

<style scoped>
/* .login__logo {
  width: 200px; height: 100px; object-fit: contain;
} */
</style>