import 'dart:convert';

import 'package:frontend/models/user_credentials.dart';
import 'package:http/http.dart' as http;
import '../models/auth_result.dart';

class UserService {
  static Future<AuthResult> login(UserCredentials userCredentials) async {
    final response = await http.post(
      Uri.parse('http://192.168.1.7:8080/api/user/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(userCredentials),
    );

    if (response.statusCode == 200) {
      return AuthResult.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Failed to login');
    }
  }
}
