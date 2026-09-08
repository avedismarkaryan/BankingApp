import 'dart:convert';
import 'package:http/http.dart' as http;
import 'auth_service.dart';

class TransferService {
  final String apiUrl = 'http://localhost:5036/api/accounts/transfer';
  final AuthService _authService = AuthService();

  Future<bool> transfer(int fromId, int toId, double amount) async {
    final token = await _authService.getToken();

    final response = await http.post(
      Uri.parse(apiUrl),
      headers: {
        'Authorization': 'Bearer $token',
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        'fromAccountId': fromId,
        'toAccountId': toId,
        'amount': amount,
      }),
    );

    return response.statusCode == 200;
  }
}