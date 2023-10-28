import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:mind_monitoring_application/controllers/login_controller.dart';
import 'package:mind_monitoring_application/views/pages/notification_screen.dart';
import 'package:tiengviet/tiengviet.dart';

import '../../controllers/home_controller.dart';
import '../../datas/globalData.dart';
import 'add_device_screen.dart';
import 'detail_device_screen.dart';
import 'setting_screen.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({Key? key}) : super(key: key);

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  TextEditingController? _textEditingController = TextEditingController();
  HomeController homeController = Get.put(HomeController());
  LoginController loginController = Get.find();
  GlobalData globalData = Get.find();
  List<Widget> listDataFlow = <Widget>[].obs;
  late FocusNode myFocusNode;
  List<String> addressListOnSearch = [];
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    homeController.fetchValueDataInDay();
    myFocusNode = FocusNode();
  }

  @override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    myFocusNode.dispose();
  }

  List<String> findDevice(List<String> listStation, value){
    List<String> addressListOnSearch = listStation
      .where((element) => TiengViet.parse(element).toLowerCase().contains(TiengViet.parse(value).toLowerCase())).toList();
    return addressListOnSearch;
  }

  Future<void> _refresh() {
    homeController.fetchValueDataInDay();
    return Future.delayed(
      Duration(milliseconds: 2),
    );
  }

  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.of(context).size.width;
    return Obx(
      () => Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.white,
          leadingWidth: 0,
          elevation: 0,
          title: Row(
            children: [
              ElevatedButton(
                onPressed: () {
                  Get.to(SettingScreen());
                },
                style: ElevatedButton.styleFrom(
                    primary: Colors.white,
                    visualDensity: VisualDensity(horizontal: 0, vertical: 0),
                    padding: EdgeInsets.all(0),
                    minimumSize: Size(30, 30),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15))),
                child: Container(
                  child: Icon(
                    Icons.account_circle_outlined,
                    size: 30,
                    color: Colors.indigo[900],
                  ),
                ),
              ),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    "Chào mừng quay trở lại!",
                    style: TextStyle(
                      color: Color.fromARGB(255, 115, 152, 190),
                      fontWeight: FontWeight.bold,
                      fontSize: 15,
                    ),
                  ),
                  Text(
                    loginController.userNameLogin.value,
                    style: TextStyle(
                        fontSize: 20,
                        color: Colors.indigo[900],
                        fontWeight: FontWeight.bold),
                  ),
                ],
              ),
              Spacer(),
              ElevatedButton(
                onPressed: () {
                  Get.dialog(
                      Center(
                        child: Stack(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 3, right: 3, top: 30, bottom: 30),
                              child: ClipRRect(
                                borderRadius: BorderRadius.circular(20),
                                child: NotificationScreen()
                              )
                            ),
                          ],
                        )
                      ),
                    );
                },
                style: ElevatedButton.styleFrom(
                    primary: Colors.white,
                    visualDensity: VisualDensity(horizontal: 0, vertical: 0),
                    padding: EdgeInsets.all(0),
                    minimumSize: Size(30, 30),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15))),
                child: Icon(
                  Icons.notifications_outlined,
                  color: Colors.indigo[900],
                  size: 25,
                ),
              ),
            ],
          ),
        ),
        body: Column(
          children: [
            Container(
              height: 45,
              margin: EdgeInsets.only(left: 15, top: 10, bottom: 10, right: 15),
              padding: const EdgeInsets.only(bottom: 2, left: 20, right: 10),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(20),
                border: Border.all(width: 1.0, color: Colors.indigo[900]!),
                color: Colors.white,
                                          boxShadow: [
                                            BoxShadow(
                                              color: Colors.indigo.withOpacity(0.2),
                                              blurRadius: 10,
                                              offset: Offset(0, 5),
                                            ),
                                          ]
              ),
              child: Row(
                children: [
                  Expanded(
                    child: TextField(
                      onChanged: (value) {
                        setState(() {
                          addressListOnSearch = findDevice(homeController.listDevice, value);
                        });
                      },
                      maxLines: 1,
                      focusNode: myFocusNode,
                      textAlignVertical: TextAlignVertical.center,
                      controller: _textEditingController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        errorBorder: InputBorder.none,
                        enabledBorder: InputBorder.none,
                        focusedBorder: InputBorder.none,
                        hintText: 'Nhập tên thiết bị để tìm kiếm!',
                        hintStyle: const TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w400,
                            color: Colors.black45),
                        suffixIcon: _textEditingController!.text.isNotEmpty
                            ? IconButton(
                                onPressed: () {
                                  _textEditingController!.clear();
                                  //FocusScope.of(context).unfocus();
                                  setState(() {});
                                },
                                icon: Icon(Icons.cancel, color: Colors.grey))
                            : null,
                      ),
                    ),
                  ),
                  Icon(
                    Icons.search,
                    color: Colors.black,
                    size: 25,
                  )
                ],
              ),
            ),
// List Device
            Container(
              alignment: Alignment.centerLeft,
              padding: EdgeInsets.only(left: 20, bottom: 5),
              child: Row(
                children: [
                  Text(
                    "Thiết bị giảm sát",
                    style: TextStyle(
                        color: Colors.black,
                        fontSize: 17,
                        fontWeight: FontWeight.w500),
                  ),
                  SizedBox(
                    width: 5,
                  ),
                  Container(
                    width: 23,
                    height: 23,
                    decoration: BoxDecoration(
                      color: Colors.blue,
                      borderRadius: BorderRadius.circular(5),
                    ),
                    child: Center(
                      child: Text(
                        homeController.loadingScreen.value ? "0" : (_textEditingController!
                                                    .text.isNotEmpty
                                                ? (addressListOnSearch.length)
                                                : (homeController
                                                    .listDevice.length)).toString(),
                        style: TextStyle(
                          color: Colors.white,
                          fontSize: 17,
                        ),
                      ),
                    ),
                  )
                ],
              ),
            ),
            Expanded(
              child: RefreshIndicator(
                displacement: 100,
                onRefresh: _refresh,
                color: Colors.indigo[900],
                child: SingleChildScrollView(
                  physics: BouncingScrollPhysics(),
                  keyboardDismissBehavior:
                      ScrollViewKeyboardDismissBehavior.onDrag,
                  padding: EdgeInsets.only(left: 10, right: 10, bottom: 10),
                  child: Container(
                    child: homeController.loadingScreen.value
                      ? Container(
                        height: MediaQuery.of(context).size.height-260,
                        child: Center(
                          child: CircularProgressIndicator(
                              color: Colors.indigo[900],
                            ),
                        ),
                      ):Container(
                      child: _textEditingController!.text.isNotEmpty &&
                              addressListOnSearch.isEmpty
                          ? Container(
                        height: MediaQuery.of(context).size.height-260,
                              child: Column(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  Icon(
                                    Icons.search_off,
                                    size: 50,
                                    color: Colors.indigo[900],
                                  ),
                                  Text(
                                    'Không tìm thấy thiết bị',
                                    style: TextStyle(
                                      fontSize: 20,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                ],
                              ),
                            )
                          : Container(
                        constraints: BoxConstraints(
                            minHeight: MediaQuery.of(context).size.height -100),
                            child: ListView.builder(
                                shrinkWrap: true,
                                physics: NeverScrollableScrollPhysics(),
                                itemCount: _textEditingController!
                                                        .text.isNotEmpty
                                                    ? (addressListOnSearch.length)
                                                    : (homeController
                                                        .listDevice.length),
                                itemBuilder: (context, index) => Container(
                                  height: 100,
                                      margin: EdgeInsets.only(top: 5, bottom: 5),
                                      decoration: BoxDecoration(
                                          color: Colors.white,
                                          borderRadius: BorderRadius.circular(20),
                                          boxShadow: [
                                            BoxShadow(
                                              color: Colors.indigo.withOpacity(0.2),
                                              blurRadius: 5,
                                              spreadRadius: 2,
                                              offset: Offset(0, 0),
                                            ),
                                          ]),
                                      child: ElevatedButton(
                                        style: ElevatedButton.styleFrom(
                                            primary: Colors.white,
                                            onPrimary: Colors.blue[300],
                                            visualDensity: VisualDensity(
                                                horizontal: 0, vertical: 0),
                                            padding: EdgeInsets.all(0),
                                            shape: RoundedRectangleBorder(
                                                borderRadius:
                                                    BorderRadius.circular(10))),
                                        onPressed: () {
                                          homeController.indexDevice.value = _textEditingController!
                                                        .text.isNotEmpty
                                                    ? (addressListOnSearch[index])
                                                    : (homeController
                                                        .listDevice[index]);
                                          Get.to(DetailDeviceScreen());
                                        },
                                        child: Column(
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
                                          children: [
                                            Container(
                                              padding: EdgeInsets.only(top: 10, left: 10, right: 5),
                                              alignment: Alignment.centerLeft,
                                              child: Text(
                                                _textEditingController!
                                                        .text.isNotEmpty
                                                    ? (addressListOnSearch[index])
                                                    : (homeController
                                                        .listDevice[index]),
                                                style: TextStyle(
                                                  color: Colors.blue[900],
                                                  fontSize: 17,
                                                ),
                                              ),
                                            ),
                                          ],
                                        ),
                                      ),
                                    )),
                          ),
                    ),
                  ),
                ),
              ),
            )
          ],
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: (){
            Get.dialog(
                      Center(
                        child: Stack(
                          children: [
                            Container(
                              margin: EdgeInsets.only(left: 5, right: 5, top: 40, bottom: 30),
                              child: ClipRRect(
                                borderRadius: BorderRadius.circular(20),
                                child: AddDeviceScreen()
                              )
                            ),
                          ],
                        )
                      ),
                      barrierDismissible: false
                    );
          },
          backgroundColor: Colors.red[900],
          child: Icon(Icons.add),
        ),
      ),
    );
  }
}
