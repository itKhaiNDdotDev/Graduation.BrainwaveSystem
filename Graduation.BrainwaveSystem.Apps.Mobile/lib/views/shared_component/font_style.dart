import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:get/get.dart';

class CustomFontStyle{
static Widget functionForAdmin(icon, name, childFunction, function) {
    return Container(
        margin: const EdgeInsets.all(10),
        alignment: Alignment.centerLeft,
        child: TextButton(
          onPressed: () {
            function();
          },
          style: TextButton.styleFrom(
            primary: Colors.indigo[300],
            backgroundColor: Colors.white,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(15),
            ),
          ),
          child: Container(
            height: 40,
            padding:
                const EdgeInsets.only(left: 10, right: 10, top: 5, bottom: 5),
            child: Row(
              children: [
                icon,
                const SizedBox(
                  width: 20,
                ),
                Text(
                  name,
                  style: TextStyle(
                    fontSize: 20,
                    color: Colors.black,
                  ),
                ),
                const Spacer(),
                childFunction
              ],
            ),
          ),
        ));
  }

  static Widget buildLoadingButonHomeScreen(bool isDone) {
    return Container(
      decoration: BoxDecoration(
        shape: BoxShape.circle,
        color: isDone ? Colors.green : Colors.indigo[900],
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
}