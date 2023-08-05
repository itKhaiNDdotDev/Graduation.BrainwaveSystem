import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../controllers/login_controller.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({Key? key}) : super(key: key);

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

enum ButtonState { init, loading, done }

class _LoginScreenState extends State<LoginScreen> {
  DateTime pre_backpress = DateTime.now();
  bool isAnimating = true;
  ButtonState state = ButtonState.init;
  var _passwordError = ''.obs;
  var _nameError = ''.obs;
  var isHiddenPassword = true.obs;
  final LoginController controller = Get.find();

  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.of(context).size.width;
    final height = MediaQuery.of(context).size.height;
    final isDone = state == ButtonState.done;
    final isStretched = isAnimating || state == ButtonState.init;

    return WillPopScope(
      onWillPop: () async {
        final timegap = DateTime.now().difference(pre_backpress);
        final cantExit = timegap >= Duration(seconds: 2);
        pre_backpress = DateTime.now();
        if (cantExit) {
          Get.closeAllSnackbars();
          Get.snackbar(
            "BrwS",
            "Nhấn lần nữa để thoát ứng dụng",
            isDismissible: true,
            duration: Duration(seconds: 2),
          );
          return false; // false will do nothing when back press
        } else {
          return true; // true will exit the app
        }
      },
      child: Scaffold(
        backgroundColor: Colors.white,
          body: SingleChildScrollView(
              keyboardDismissBehavior: ScrollViewKeyboardDismissBehavior.onDrag,
              child: Form(
                autovalidateMode: AutovalidateMode.onUserInteraction,
                child: Column(
                  children: [
// Tên ứng dụng
                    Container(
                      margin: EdgeInsets.only(
                          left: 5,
                          top: 150,
                          right: 5),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Image.asset(
                            'assets/logo.png',
                            height: 60,
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                "BrwS",
                                style: TextStyle(
                                  fontSize: 22,
                                  color: Colors.indigo[900],
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                              Text(
                                "HỆ THỐNG GIÁM SÁT",
                                style: TextStyle(
                                  fontSize: 21,
                                  color: Colors.grey,
                                  fontWeight: FontWeight.bold
                                ),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
// Nhập tên đăng nhập
                    Container(
                          margin: EdgeInsets.only(left: 20, right: 20, top: 90),
                          padding: EdgeInsets.only(left: 20, right: 20),
                          width: width > 700 ? 450 : null,
                          height: _nameError.value==''? 50:70,
                          child: Obx(
                              () =>  TextField(
                              controller: controller.userNameController,
                              onChanged: (value) {
                                if (controller.userNameController.text.length < 1) {
                                        _nameError.value =
                                            "Tên đăng nhập không được để trống";
                                      } else {
                                        _nameError.value='';
                                      }
                              },
                              style: TextStyle(
                                color: Colors.indigo[900],
                                fontSize: 15,
                              ),
                              decoration: InputDecoration(
                                prefixIcon: Icon(
                                  Icons.account_circle_outlined,
                                  color: Colors.indigo[900],
                                ),
                                labelText: "Tên đăng nhập",
                                errorText: _nameError.value == ''
                                    ? null
                                    : _nameError.value,
                                labelStyle: TextStyle(
                                  color: Colors.grey[600],
                                  fontSize: 15,
                                ),
                                border: InputBorder.none,
                                focusedErrorBorder: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(25),
                                    borderSide:
                                        BorderSide(color: Color(0xFF1A237E))),
                                errorBorder: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(25),
                                    borderSide:
                                        BorderSide(color: Color(0xFF1A237E))),
                                enabledBorder: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(25),
                                    borderSide:
                                        BorderSide(color: Color(0xFFEEEEEE))),
                                focusedBorder: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(25),
                                    borderSide:
                                        BorderSide(color: Color(0xFF1A237E))),
                                filled: true,
                              ),
                              obscureText: false,
                            ),
                          ),
                        ),
// Mật khẩu
                        Container(
                          width: width > 700 ? 450 : null,
                          height: _passwordError.value==''? 50:70,
                          margin: EdgeInsets.only(left: 20, right: 20, top: 10),
                          padding: EdgeInsets.only(left: 20, right: 20),
                          child: Obx(
                              () => TextField(
                                controller: controller.passwordController,
                                onChanged: (value) {
                                if(controller.passwordController.text.length<1){
                                        _passwordError.value ="Mật khẩu không được để trống";
                                      }else{
                                        _passwordError.value ="";
                                      }
                              },
                                style: TextStyle(
                                  color: Colors.indigo[900],
                                  fontSize: 15,
                                ),
                                decoration: InputDecoration(
                                    prefixIcon: Icon(Icons.lock_outline_rounded,
                                        color: Colors.indigo[900]),
                                    labelText: "Mật khẩu",
                                    errorText: _passwordError.value == ''
                                        ? null
                                        : _passwordError.value,
                                    labelStyle: TextStyle(
                                      color: Colors.grey[600],
                                      fontSize: 15,
                                    ),
                                    border: InputBorder.none,
                                    focusedErrorBorder: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(25),
                                        borderSide:
                                            BorderSide(color: Color(0xFF1A237E))),
                                    errorBorder: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(25),
                                        borderSide:
                                            BorderSide(color: Color(0xFF1A237E))),
                                    enabledBorder: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(25),
                                        borderSide:
                                            BorderSide(color: Color(0xFFEEEEEE))),
                                    focusedBorder: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(25),
                                        borderSide:
                                            BorderSide(color: Color(0xFF1A237E))),
                                    filled: true,
                                    suffixIcon: InkWell(
                                      onTap: () {
                                        isHiddenPassword.value =
                                            !isHiddenPassword.value;
                                      },
                                      child: Icon(
                                        isHiddenPassword.value
                                            ? Icons.visibility
                                            : Icons.visibility_off,
                                        color: Colors.indigo[900],
                                      ),
                                    )),
                                obscureText: isHiddenPassword.value,
                              ),
                          ),
                        ),
// Quên mật khẩu
                        Container(
                           margin: EdgeInsets.only(
                          left: 20, right: 20, top: 15, bottom: 15),
                          padding: EdgeInsets.only(left: 20, right: 20),
                          width: width > 700 ? 450 : null,
                          alignment: Alignment.centerRight,
                          child: GestureDetector(
                            onTap: () {
                              FocusScope.of(context).unfocus();
                              Get.closeAllSnackbars();
                              Get.snackbar(
                                "BrwS",
                                "Chức năng đang phát triển",
                                isDismissible: true,
                              );
                            },
                            child: Text(
                              "Quên mật khẩu?",
                              style: TextStyle(
                                color: Colors.indigo[900],
                                fontSize: 15,
                              ),
                            ),
                          ),
                        ),
// Đăng nhập
                        Container(
                            width: width > 700 ? 450 : null,
                            padding: const EdgeInsets.fromLTRB(40, 0, 40, 30),
                            child: AnimatedContainer(
                                duration: Duration(milliseconds: 500),
                                curve: Curves.easeIn,
                                height: 50,
                                width: state == ButtonState.init ? width : 50,
                                onEnd: () =>
                                    setState(() => isAnimating = !isAnimating),
                                child: isStretched
                                    ? ElevatedButton(
                                        onPressed: () async {
                                          FocusScope.of(context).unfocus();
                                          if (controller
                                                        .userNameController
                                                        .text
                                                        .length <
                                                    1) {
                                                  _nameError.value =
                                                      "Tên đăng nhập không được để trống";
                                                  if (controller
                                                          .passwordController
                                                          .text
                                                          .length <
                                                      1) {
                                                    _passwordError.value =
                                                        "Mật khẩu không được để trống";
                                                  } else {
                                                    _passwordError.value = '';
                                                  }
                                                } else if (controller
                                                        .passwordController
                                                        .text
                                                        .length <
                                                    1) {
                                                  _nameError.value = '';
                                                  _passwordError.value =
                                                      "Mật khẩu không được để trống";
                                                } else {
                                                  _passwordError.value = '';
                                                  _nameError.value = '';
                                                  setState(() {
                                            state = ButtonState.loading;
                                          });
                                          await Future.delayed(
                                              Duration(seconds: 1));
                                          if (await controller.checkLogin()) {
                                            final preferences =
                                                await SharedPreferences
                                                    .getInstance();
                                            preferences.setString(
                                                'username',
                                                controller
                                                    .userNameController.text);
                                            preferences.setString(
                                                'password',
                                                controller
                                                    .passwordController.text);
                                            setState(() {
                                              state = ButtonState.done;
                                            });
                                            //Get.put(StationController());

                                            await Future.delayed(
                                                Duration(seconds: 1));
                                            Get.offNamed('/navigation');
                                          } else {
                                            setState(() {
                                              state = ButtonState.init;
                                            });
                                            Get.closeAllSnackbars();
                                            Get.snackbar(
                                              "BrwS",
                                              "Tên đăng nhập hoặc mật khẩu sai!",
                                              isDismissible: true,
                                            );
                                          }
                                                }
                                          
                                        },
                                        child: FittedBox(
                                          child: Text("Đăng nhập",
                                              style: TextStyle(
                                                  fontSize: 20,
                                                  fontWeight: FontWeight.bold)),
                                        ),
                                        style: ElevatedButton.styleFrom(
                                            primary: Colors.indigo[900],
                                            shape: RoundedRectangleBorder(
                                                borderRadius:
                                                    BorderRadius.circular(25))))
                                    : buildLoadingButton(isDone))),
                    Container(
                      child: Text(
                        "BrwS @ " +
                            DateTime.now().year.toString(),
                        style: TextStyle(color: Colors.grey),
                      ),
                    )
                  ],
                ),
              ))),
    );
  }
}

Widget buildLoadingButton(bool isDone) {
  return Container(
    decoration: BoxDecoration(
      shape: BoxShape.circle,
      color: Colors.indigo[900],
    ),
    child: Center(
      child: isDone
          ? Icon(
              Icons.done,
              size: 30,
              color: Colors.white,
            )
          : CircularProgressIndicator(
              color: Colors.white,
            ),
    ),
  );
}
