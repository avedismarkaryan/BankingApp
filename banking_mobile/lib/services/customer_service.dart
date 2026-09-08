import 'dart:convert';
import 'package:http/http.dart' as http;
import 'auth_service.dart';

class Account {
  final int id;
  final String owner;
  final double balance;

  Account({required this.id, required this.owner, required this.balance});

  factory Account.fromJson(Map<String, dynamic> json) {
    return Account(
      id: json['id'],
      owner: json['owner'],
      balance: (json['balance'] as num).toDouble(),
    );
  }
}

class CustomerService {
  final String apiUrl = 'http://localhost:5036/api/accounts';
  final AuthService _authService = AuthService();

  Future<List<Account>> getAll() async {
    final token = await _authService.getToken();

    final response = await http.get(
      Uri.parse(apiUrl),
      headers: {'Authorization': 'Bearer $token'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> data = jsonDecode(response.body);
      return data.map((json) => Account.fromJson(json)).toList();
    } else {
      throw Exception('Hesaplar yüklenemedi: ${response.statusCode}');
    }
  }
}