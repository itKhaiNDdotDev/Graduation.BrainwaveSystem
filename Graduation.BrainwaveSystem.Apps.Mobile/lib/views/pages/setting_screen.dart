import 'package:app_settings/app_settings.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:get/get.dart';
import '../../controllers/login_controller.dart';
import '../shared_component/font_style.dart';
import 'dart:io' show Platform;
import 'package:flutter/foundation.dart' show kIsWeb;

class SettingScreen extends StatefulWidget {
  const SettingScreen({Key? key}) : super(key: key);

  @override
  _SettingScreenState createState() => _SettingScreenState();
}

class _SettingScreenState extends State<SettingScreen>
    with WidgetsBindingObserver {
  DateTime pre_backpress = DateTime.now();
  LoginController controller = Get.find();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) async {
    if (!kIsWeb) {
      if (Platform.isAndroid || Platform.isIOS) {
        final _preferences = await SharedPreferences.getInstance();
        if (state == AppLifecycleState.resumed) {
          NotificationSettings settings = await FirebaseMessaging.instance
              .requestPermission(
            alert: true,
            announcement: true,
            badge: true,
            carPlay: true,
            criticalAlert: true,
            provisional: true,
            sound: true,
          )
              .then((value) {
            if (value.authorizationStatus == AuthorizationStatus.authorized ||
                value.authorizationStatus == AuthorizationStatus.provisional) {
              FirebaseMessaging.instance.subscribeToTopic("AIKH");
              FirebaseMessaging.instance
                  .subscribeToTopic(controller.userNameLogin.value);
              print("user granted permission");
              setState(() {
                controller.statusNotification.value = true;
              });
              _preferences.setBool(
                  "statusNotification", controller.statusNotification.value);
            } else {
              FirebaseMessaging.instance
                  .unsubscribeFromTopic(controller.userNameLogin.value);
              FirebaseMessaging.instance.unsubscribeFromTopic("AIKH");
              setState(() {
                controller.statusNotification.value = false;
              });
              _preferences.setBool(
                  "statusNotification", controller.statusNotification.value);
            }
            return value;
          });
        }
      }
    }
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  void functionNotification() async {
    final _preferences = await SharedPreferences.getInstance();
    if (!kIsWeb) {
      if (Platform.isAndroid || Platform.isIOS) {
        if (!controller.statusNotification.value) {
          NotificationSettings settings = await FirebaseMessaging.instance
              .requestPermission(
            alert: true,
            announcement: true,
            badge: true,
            carPlay: true,
            criticalAlert: true,
            provisional: true,
            sound: true,
          )
              .then((value) {
            if (value.authorizationStatus == AuthorizationStatus.authorized ||
                value.authorizationStatus == AuthorizationStatus.provisional) {
              FirebaseMessaging.instance.subscribeToTopic("AIKH");
              FirebaseMessaging.instance
                  .subscribeToTopic(controller.userNameLogin.value);
              print("user granted permission");
              setState(() {
                controller.statusNotification.value = true;
              });
              _preferences.setBool(
                  "statusNotification", controller.statusNotification.value);
            } else {
              AppSettings.openNotificationSettings();
            }
            return value;
          });
        } else {
          FirebaseMessaging.instance
              .unsubscribeFromTopic(controller.userNameLogin.value);
          FirebaseMessaging.instance.unsubscribeFromTopic("AIKH");
          controller.statusNotification.value = false;
          _preferences.setBool(
              "statusNotification", controller.statusNotification.value);
        }
      }
    }
    controller.statusNotification.value = !controller.statusNotification.value;
    _preferences.setBool(
        "statusNotification", controller.statusNotification.value);
  }

  void functionLogout() async {
    final timegap = DateTime.now().difference(pre_backpress);
    final cantExit = timegap >= Duration(seconds: 2);
    pre_backpress = DateTime.now();
    if (cantExit) {
      Get.closeAllSnackbars();
      Get.snackbar(
        "AIKH",
        "Nhấn lần nữa để đăng xuất!",
        isDismissible: true,
        duration: Duration(seconds: 2),
      );
      // false will do nothing when back press
    } else {
      FirebaseMessaging.instance
          .unsubscribeFromTopic(controller.userNameLogin.value);
      controller.passwordController.clear();
      controller.userNameController.clear();
      controller.userName = "";
      controller.password = "";
      controller.isLoading.value = false;
      controller.isDone.value = false;
      final preferences = await SharedPreferences.getInstance();
      preferences.remove('username');
      preferences.remove('password');
      Get.offNamed('/login'); // true will exit the app
    }
  }

  @override
  Widget build(BuildContext context) {
    //this line is place here for testing purposes
    //
    return Scaffold(
        appBar: AppBar(
          automaticallyImplyLeading: false,
          backgroundColor: Colors.white,
          titleSpacing: 0,
          title: Row(
            children: [
              TextButton(
                onPressed: () {
                  Get.back();
                },
                style: ButtonStyle(
                  overlayColor: MaterialStateProperty.all(Colors.white),
                  minimumSize: MaterialStateProperty.all(Size.zero),
                  foregroundColor: MaterialStateProperty.resolveWith<Color>(
                      (Set<MaterialState> states) {
                    if (states.contains(MaterialState.pressed))
                      return Colors.grey;
                    return Colors
                        .indigo[900]!; // Defer to the widget's default.
                  }),
                ),
                child: Tooltip(
                  message: "Quay lại",
                  child: Icon(
                    Icons.arrow_back_ios,
                    size: 20,
                  ),
                ),
              ),
              Text(
                "Thiết lập",
                style: TextStyle(color: Colors.indigo[900], fontSize: 17),
              ),
            ],
          ),
        ),
        body: Container(
          color: Color.fromARGB(143, 224, 224, 224),
          child: Column(
            children: [
              Container(
                margin: const EdgeInsets.only(bottom: 10),
                alignment: Alignment.centerLeft,
                child: ElevatedButton(
                  onPressed: () {},
                  style: ElevatedButton.styleFrom(
                    padding: EdgeInsets.all(0),
                    primary: Colors.white,
                  ),
                  child: Container(
                    height: 80,
                    padding: const EdgeInsets.only(
                        left: 10, right: 10, top: 5, bottom: 5),
                    child: Row(
                      children: [
                        Icon(
                          Icons.account_circle_outlined,
                          color: Colors.indigo[900],
                          size: 40,
                        ),
                        const SizedBox(
                          width: 20,
                        ),
                        Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            Text(
                              "Người dùng:",
                              style: TextStyle(
                                  fontSize: 20,
                                  color: Colors.black,
                                  fontWeight: FontWeight.w500),
                            ),
                            Obx(
                              () => Text(
                                controller.userNameLogin.value,
                                style: TextStyle(
                                  color: Colors.black54,
                                  fontSize: 15,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ],
                    ),
                  ),
                ),
              ),
// Thông báo
              Obx(
                () => CustomFontStyle.functionForAdmin(
                    Icon(
                      Icons.notifications_none_sharp,
                      color: Colors.indigo[900],
                      size: 25,
                    ),
                    'Thông báo',
                    SizedBox(
                      height: 20,
                      child: CupertinoSwitch(
                          value: controller.statusNotification.value,
                          activeColor: Colors.indigo[900],
                          onChanged: (bool newValue) {
                            functionNotification();
                          }),
                    ),
                    functionNotification),
              ),
              CustomFontStyle.functionForAdmin(
                  Icon(
                    Icons.power_settings_new,
                    color: Colors.red,
                    size: 25,
                  ),
                  "Đăng xuất",
                  Icon(
                    Icons.arrow_forward_ios,
                    color: Colors.indigo[900],
                    size: 20,
                  ),
                  functionLogout),
            ],
          ),
        ));
  }
}
