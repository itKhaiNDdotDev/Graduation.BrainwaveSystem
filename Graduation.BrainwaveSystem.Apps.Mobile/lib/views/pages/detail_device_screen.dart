import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:mind_monitoring_application/controllers/home_controller.dart';


class DetailDeviceScreen extends StatefulWidget {
  const DetailDeviceScreen({Key? key}) : super(key: key);

  @override
  State<DetailDeviceScreen> createState() => _DetailDeviceScreenState();
}

class _DetailDeviceScreenState extends State<DetailDeviceScreen> with TickerProviderStateMixin {
  late TabController tabController;
  HomeController homeController = Get.find();
  @override
  void initState() {
    super.initState();
    tabController =
        TabController(length: 4, vsync: this);
    tabController.addListener(() {
      
    });
    //controlStationController.connectMQTT('mqtt.abcsolutions.com.vn', 'monitoring_mobile', 1883, "abcsolution", "CseLAbC5c6");
  }
  @override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    //controlStationController.disconnectMQTT();
  }
  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.of(context).size.width;
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        automaticallyImplyLeading: false,
        backgroundColor: Colors.white,
        elevation: 0,
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
                  return Colors.indigo[900]!; // Defer to the widget's default.
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
              homeController.indexDevice.value,
              style: TextStyle(color: Colors.indigo[900], fontSize: 17),
            ),
          ],
        ),
      ),
       body:Column(
         children: [
          Container(
            height: 35,
            child: TabBar(
              controller: tabController,
              isScrollable: true,
              labelColor: Colors.indigo[900],
              unselectedLabelColor: Colors.grey,
              padding: EdgeInsets.only(bottom: 5),
              indicatorSize: TabBarIndicatorSize.label,
              indicatorColor: Colors.indigo[900],
              indicatorWeight: 4,
              tabs: [
                Tab(
                  text: "GENERAL",
                ),
                Tab(
                  text: "TGAM EXTRACTION",
                ),
                Tab(
                  text: "RAW EEF DATA",
                ),
                Tab(
                  text: "MODELS RESULT",
                )
              ],
            ),
          ),
           Expanded(
             child: TabBarView(
              controller: tabController,
               children: [
                Container(
                  color: Colors.white,
                ),
                Container(
                  color: Colors.white,
                ),
                Container(
                  color: Colors.white,
                ),
                Container(
                  color: Colors.white,
                )
               ],
             ),
           ),
         ],
       ),
    );
  }
}