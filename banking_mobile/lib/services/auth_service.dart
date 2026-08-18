import 'dart:convert';
import 'package:http/http.dart' as http;

class AuthService {

  final String baseURL = 'http://localhost:5036/api/auth';

  Future<String?> login(String email, String password) async {
    final response = await http.post(
      Uri.parse('$baseURL/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'password': password})
    );

    if (response.statusCode == 200){
      final data = jsonDecode(response.body);
      return data['token'];

    } else{
      return null;
    } 
  }
}