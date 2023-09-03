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

    if (response.statusCode == 200) {
      return AuthResult.fromJson(jsonDecode(response.body));
    } else {
      throw AuthResult("", "Failed to login", false);
    }
  }
}
