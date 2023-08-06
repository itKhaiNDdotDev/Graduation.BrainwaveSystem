import 'package:http/http.dart' as http;

// What this file does
// - Open client persistent connection
// - Close client persistent connection
// - provide singleton instance of client persistent connection (make sure there is only one instance of client persistent connection)
class ConnectionService {
  static final client = http.Client();

  //------------------------------------------------------//
  //--------Make this class become a singleton------------//
  static final _singleton = ConnectionService._internal();

  factory ConnectionService() {
    return _singleton;
  }

  ConnectionService._internal();

  //------------------------------------------------------//

  //like other normal class from this point
  static void onClose() {
    client.close();
  }
}
