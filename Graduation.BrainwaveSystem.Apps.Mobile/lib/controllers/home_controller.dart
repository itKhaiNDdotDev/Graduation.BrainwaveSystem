
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../datas/globalData.dart';
import '../services/value_service.dart';
import '../views/pages/home_screen.dart';
import 'login_controller.dart';

enum HomeScreenState {initScreen, reportScreen, notificationScreen, intormationScreen}
enum ButtonState {init, loading, done}
class HomeController extends GetxController{
  var homeScreenState = HomeScreenState.initScreen.obs;
  LoginController loginController = Get.find();
  GlobalData globalData = Get.find();
  var loadingScreen = true.obs;
  var indexDevice = "".obs;
  List<String> listDevice = [
    "KhaiND Demo1",
    "Test Draft 0",
    "string",
    "MindLink HC05"
  ];
 

  
  Future<bool> fetchValueDataInDay() async {
    loadingScreen.value = true;
    await Future.delayed(Duration(milliseconds: 500));
    var newAccessToken =
        await ValueService.fetchAllValue(loginController.userName, DateTime.now());
    if (newAccessToken != null) {
      loadingScreen.value = false;
      return true;
    } 
    return false;
  }

}