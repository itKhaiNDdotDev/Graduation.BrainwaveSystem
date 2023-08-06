import 'package:flutter/material.dart';
import 'package:get/get.dart';

class AddDeviceController extends GetxController{
  TextEditingController name = TextEditingController();
  TextEditingController description = TextEditingController();
  TextEditingController status = TextEditingController();
  var nameError = "".obs;
  var statusError = "".obs;
  checkRegisterNameError(){
    if(name.text.isEmpty){
      nameError.value ="Không được để trống";
      return false;
    }else{
      nameError.value = "";
      return true;
    }
  }

  checkRegisterStatusError(){
    if(status.text.isEmpty){
      statusError.value ="Không được để trống";
      return false;
    }else{
      statusError.value = "";
      return true;
    }
  }
}