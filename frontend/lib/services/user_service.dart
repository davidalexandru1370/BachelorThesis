import 'dart:convert';

import 'package:frontend/domain/constants/api_constants.dart';
import 'package:frontend/domain/models/user_credentials.dart';
import 'package:http/http.dart' as http;
import '../domain/models/auth_result.dart';

class UserService {
  static Future<AuthResult> login(UserCredentials userCredentials) async {
    final response = await http.post(
      Uri.parse('${ApiConstants.BASE_URL}/user/login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(userCredentials),
    );

    if (response.statusCode >= 500) {
      throw AuthResult("", "Failed to register", false);
    }

    var authResult = AuthResult.fromJson(jsonDecode(response.body));
    if (response.statusCode == 200) {
      return authResult;
    } else {
      throw authResult;
    }
  }

  static Future<AuthResult> register(UserCredentials userCredentials) async {
    final response = await http.post(
      Uri.parse('${ApiConstants.BASE_URL}/user/register'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(userCredentials),
    );

    if (response.statusCode >= 500) {
      throw AuthResult("", "Failed to register", false);
    }

    var authResult = AuthResult.fromJson(jsonDecode(response.body));
    if (response.statusCode == 200) {
      return authResult;
    } else {
      throw authResult;
    }
  }
}
