import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:mind_monitoring_application/views/shared_component/font_style.dart';
import 'package:status_alert/status_alert.dart';

import '../../controllers/home_controller.dart';
import '../../controllers/notification_controller.dart';

class NotificationScreen extends StatefulWidget {
  const NotificationScreen({Key? key}) : super(key: key);

  @override
  State<NotificationScreen> createState() => _NotificationScreenState();
}

class _NotificationScreenState extends State<NotificationScreen>
    with TickerProviderStateMixin {
  NotificationController notificationController = Get.put(NotificationController());
  late TabController _tabController;
  _showAlertDialog(BuildContext context) {
    showCupertinoModalPopup<void>(
      context: context,
      builder: (BuildContext context) => CupertinoAlertDialog(
        title: const Text('XÁC NHẬN'),
        content: const Text('Đã xem tất cả thông báo!'),
        actions: <CupertinoDialogAction>[
          CupertinoDialogAction(
            isDestructiveAction: true,
            onPressed: () {
              Navigator.pop(context);
            },
            child: const Text('Hủy'),
          ),
          CupertinoDialogAction(
            isDestructiveAction: false,
            onPressed: () async {
              // if (await notificationController.updateAllNotification()) {
              //   StatusAlert.show(
              //     context,
              //     duration: Duration(seconds: 1),
              //     subtitle: "Thành công!",
              //     configuration: IconConfiguration(icon: Icons.done, size: 60),
              //     maxWidth: 150,
              //   );
              // } else {
              //   StatusAlert.show(
              //     context,
              //     duration: Duration(seconds: 1),
              //     subtitle: "Thất bại!",
              //     configuration: IconConfiguration(
              //         icon: Icons.dangerous_outlined, size: 60),
              //     maxWidth: 150,
              //   );
              // }
             Navigator.pop(context);
            },
            child: const Text('Đã xem'),
          ),
        ],
      ),
    );
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    notificationController.loadingScreen.value = false;
    _tabController = TabController(length: 2, vsync: this);
    _tabController.animateTo(0);
  }

  @override
  Widget build(BuildContext context) {
    return Obx(
      () => DefaultTabController(
        length: 2,
        child: Scaffold(
          appBar: AppBar(
            toolbarHeight: 50,
            backgroundColor: Colors.white,
            automaticallyImplyLeading: false,
            elevation: 0,
            title: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                SizedBox(
                  width: 40,
                  child: TextButton(
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
                        size: 25,
                      ),
                    ),
                  ),
                ),
                Text(
                  "THÔNG BÁO",
                  style: TextStyle(
                    color: Colors.indigo[900],
                    fontSize: 20,
                  ),
                ),
                TextButton(
                  onPressed: () {
                    _showAlertDialog(context);
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
                    message: "Đã xem tất cả",
                    child: Icon(
                      Icons.assignment_turned_in_outlined,
                      size: 30,
                    ),
                  ),
                ),
              ],
            ),
          ),
          body: Column(
            children: [
              Container(
                height: 50,
                width: double.infinity,
                padding: EdgeInsets.all(5),
                margin: EdgeInsets.only(bottom: 5),
                decoration: BoxDecoration(
                  color: Colors.white,
                  boxShadow: [
                    BoxShadow(
                      color: Colors.grey.withOpacity(0.5),
                      blurRadius: 7,
                      offset: Offset(0, 10), // changes position of shadow
                    ),
                  ],
                ),
                child: TabBar(
                    controller: _tabController,
                    isScrollable: false,
                    indicator: BoxDecoration(
                        borderRadius: BorderRadius.circular(20.0),
                        color: Colors.indigo[900]),
                    labelColor: Colors.white,
                    unselectedLabelColor: Colors.black54,
                    tabs: [
                      Tab(
                        child: Text(
                          "CHƯA ĐỌC",
                          style: TextStyle(fontSize: 17),
                        ),
                      ),
                      Tab(
                        child: Text(
                          "TẤT CẢ",
                          style: TextStyle(fontSize: 17),
                        ),
                      )
                    ]),
              ),
              Expanded(
                child: TabBarView(
                  controller: _tabController,
                  children: [
                    ListNotificationFollowTab(false),
                    ListNotificationFollowTab(true),
                  ],
                ),
              )
            ],
          ),
        ),
      ),
    );
  }

  ListNotificationFollowTab(bool readedNotification) {
    Future<void> _refresh()async {
      notificationController.loadingScreen.value = true;
              await Future.delayed(Duration(milliseconds: 500));
              notificationController.loadingScreen.value = false;
      return Future.delayed(Duration(milliseconds: 0));
    }

    return RefreshIndicator(
                displacement: 100,
                onRefresh: _refresh,
                color: Colors.red,
      child: SingleChildScrollView(
                  physics: BouncingScrollPhysics(),
                  keyboardDismissBehavior:
                      ScrollViewKeyboardDismissBehavior.onDrag,
        child: Container(
                      child: notificationController.loadingScreen.value
                        ? Container(
                          height: MediaQuery.of(context).size.height-260,
                          child: Center(
                            child: CircularProgressIndicator(
                                color: Colors.indigo[900],
                              ),
                          ),
                        ):Container(
                          constraints: BoxConstraints(
                              minHeight: MediaQuery.of(context).size.height-100),
            child: ListView.builder(
              shrinkWrap: true,
              physics: NeverScrollableScrollPhysics(),
              itemCount: readedNotification
                  ? 4
                  : 6,
              itemBuilder: (context, index) {
                return Card(
                  shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(5)),
                  shadowColor: Colors.indigo.withOpacity(0.4),
                  color: Colors.white,
                  elevation: 5.0,
                  child: Padding(
                    padding: const EdgeInsets.only(left: 10, right: 5),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Row(
                          children: [
                            Padding(
                              padding: readedNotification
                                  ? const EdgeInsets.symmetric(vertical: 15)
                                  : EdgeInsets.all(0),
                              child: Text("Thông báo " + 
                                (readedNotification
                                    ? index.toString()
                                    : (index + 4).toString()),
                                style: TextStyle(color: Colors.indigo[900]),
                              ),
                            ),
                            Spacer(),
                            Container(
                              child: readedNotification
                                  ? null
                                  : TextButton(
                                      onPressed: () async {
                                      },
                                      style: ButtonStyle(
                                        overlayColor:
                                            MaterialStateProperty.all(Colors.white),
                                        minimumSize:
                                            MaterialStateProperty.all(Size.zero),
                                        foregroundColor:
                                            MaterialStateProperty.resolveWith<Color>(
                                                (Set<MaterialState> states) {
                                          if (states.contains(MaterialState.pressed))
                                            return Colors.grey;
                                          return Colors
                                              .indigo[900]!; // Defer to the widget's default.
                                        }),
                                      ),
                                      child: Tooltip(
                                        message: "Đã xem",
                                        child: Icon(
                                          Icons.clear_rounded,
                                          size: 25,
                                        ),
                                      ),
                                    ),
                            ),
                          ],
                        ),
                        Text(
                          "Nội dung thông báo",
                          style: TextStyle(color: Colors.indigo[900]),
                        ),
                        SizedBox(
                          height: 10,
                        ),
                        Text(
                         "Đang trong tình trạng ngủ",
                          style: TextStyle(color: Colors.indigo[900]),
                        ),
                        SizedBox(
                          height: 15,
                        ),
                      ],
                    ),
                  ),
                );
              },
            ),
          ),
        ),
      ),
    );
  }
}
