import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'controllers/login_controller.dart';
import 'datas/globalData.dart';
import 'firebase_options.dart';
import 'services/connection_service.dart';
import 'services/local_notification_service.dart';
import 'views/pages/check_login_screen.dart';
import 'views/pages/home_screen.dart';
import 'views/pages/login_screen.dart';
import 'dart:io' show Platform;
import 'package:flutter/foundation.dart' show kIsWeb;

Future<void> _firebaseMessagingBackgroundHandler(RemoteMessage message) async {
  // If you're going to use other Firebase services in the background, such as Firestore,
  // make sure you call `initializeApp` before using other Firebase services.
  await Firebase.initializeApp();

  print("Handling a background message: ${message.messageId}");
}

void main() async {
  if (!kIsWeb) {
    if (Platform.isAndroid || Platform.isIOS) {
      WidgetsFlutterBinding.ensureInitialized();
      await Firebase.initializeApp(
        options: DefaultFirebaseOptions.currentPlatform,
      );

      FirebaseMessaging.onBackgroundMessage(
          _firebaseMessagingBackgroundHandler);
      final _preferences = await SharedPreferences.getInstance();
      FirebaseMessaging.instance;

      FirebaseMessaging messaging = FirebaseMessaging.instance;

      NotificationSettings settings = await messaging
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
          print("user granted permission");
          if (_preferences.getBool("statusNotification") ?? true) {
            FirebaseMessaging.instance.subscribeToTopic("AIKH");
            _preferences.setBool("statusNotification", true);
          } else {
            FirebaseMessaging.instance.unsubscribeFromTopic("AIKH");
            _preferences.setBool("statusNotification", false);
          }
        } else {
          FirebaseMessaging.instance.unsubscribeFromTopic("AIKH");
          _preferences.setBool("statusNotification", false);
        }
        return value;
      });

      LocalNotificationService.initialize();
      FirebaseMessaging.onMessage.listen((RemoteMessage message) {
        print('Got a message whilst in the foreground!');
        if (message.notification != null) {
          LocalNotificationService.createAndDisplayNotification(message);
        }
      });
      FirebaseMessaging.onMessageOpenedApp.listen(
        (message) {
          print("FirebaseMessaging.onMessageOpenedApp.listen");
        },
      );
      FirebaseFirestore firestore = FirebaseFirestore.instance;
    }
  }

  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    //start client persistent connection
    ConnectionService();
    //Make globalData available for all child
    Get.put(GlobalData());
    Get.put(LoginController());
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.portraitDown,
      DeviceOrientation.portraitUp,
    ]);
  }

  @override
  dispose() {
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.landscapeRight,
      DeviceOrientation.landscapeLeft,
      DeviceOrientation.portraitUp,
      DeviceOrientation.portraitDown,
    ]);
    super.dispose();
  }

  @override
  void deactivate() {
    // TODO: implement deactivate
    super.deactivate();
    ConnectionService.onClose();
  }

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
        debugShowCheckedModeBanner: false,
        initialRoute: '/',
        getPages: [
          GetPage(name: '/', page: () => const CheckLoginScreen()),
          GetPage(name: "/login", page: () => LoginScreen()),
          GetPage(name: '/home', page: () => HomeScreen()),
        ]);
  }
}
