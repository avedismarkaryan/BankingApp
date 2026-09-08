import 'dart:convert';
import 'package:http/http.dart' as http;
import 'auth_service.dart';

class Transaction {
  final int id;
  final int accountId;
  final String type;
  final double amount;
  final double balanceAfter;
  final String description;
  final DateTime createdAt;

  Transaction({
    required this.id,
    required this.accountId,
    required this.type,
    required this.amount,
    required this.balanceAfter,
    required this.description,
    required this.createdAt,
  });

  factory Transaction.fromJson(Map<String, dynamic> json) {
    return Transaction(
      id: json['id'],
      accountId: json['accountId'],
      type: json['type'],
      amount: (json['amount'] as num).toDouble(),
      balanceAfter: (json['balanceAfter'] as num).toDouble(),
      description: json['description'] ?? '',
      createdAt: DateTime.parse(json['createdAt']),
    );
  }
}

class TransactionService {
  final String apiUrl = 'http://localhost:5036/api/transactions';
  final AuthService _authService = AuthService();

  Future<List<Transaction>> getByAccountId(int accountId) async {
    final token = await _authService.getToken();

    final response = await http.get(
      Uri.parse('$apiUrl/account/$accountId'),
      headers: {'Authorization': 'Bearer $token'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> data = jsonDecode(response.body);
      return data.map((json) => Transaction.fromJson(json)).toList();
    } else {
      throw Exception('Hareketler yüklenemedi: ${response.statusCode}');
    }
  }
}