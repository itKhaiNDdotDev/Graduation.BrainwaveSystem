import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../controllers/login_controller.dart';

class CheckLoginScreen extends StatefulWidget {
  const CheckLoginScreen({Key? key}) : super(key: key);

  @override
  State<CheckLoginScreen> createState() => _CheckLoginScreenState();
}

class _CheckLoginScreenState extends State<CheckLoginScreen> {
  startTime() async {
    var duration = Duration(seconds: 2);
    return new Timer(duration, route);
  }
  route() async{
    final LoginController controller = Get.put(LoginController());
    final preferences = await SharedPreferences.getInstance();
    controller.userNameController.text= preferences.getString('username')??'';
    controller.passwordController.text=preferences.getString('password')??'';
    if(controller.userNameController.text.length<=0 || controller.passwordController.text.length<=0 || !await controller.checkLogin()){
      Get.offNamed('/login');
    }else{
      controller.statusNotification.value = preferences.getBool("statusNotification")??false;
      controller.userNameLogin.value = controller.userNameController.text;
      Get.offNamed('/home');
    } 
  }
  @override
  void initState() {
    super.initState();
    startTime();
  }
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async{
        return false;
      },
      child: Scaffold(
        body: Stack(
          children: [
            Container(
              decoration: BoxDecoration(
                color: Colors.white,
              ),
            ),
            
            Center(
              child: SpinKitThreeBounce(
                color: Colors.indigo[900]!,
                size: 30.0,
              )
            )
          ],
        ),
      ),
    );
  }
}