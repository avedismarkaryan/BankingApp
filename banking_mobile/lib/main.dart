import 'package:flutter/material.dart';
import 'services/auth_service.dart';
import 'services/customer_service.dart';
import 'services/transaction_service.dart';
import 'services/transfer_service.dart';
import 'package:provider/provider.dart';
void main() {
  runApp(
    ChangeNotifierProvider(
      create: (_) => AuthService(),
      child: const MyApp(),
      ),
    );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Banking Mobile',
      theme: ThemeData(
      colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),
      ),
      home: const LoginPage(),
    );
  }
}

class LoginPage extends StatefulWidget {
  const LoginPage ({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class CustomerListPage extends StatefulWidget {
  const CustomerListPage({super.key});

  @override
  State<CustomerListPage> createState() => _CustomerListPageState();
}

class TransactionListPage extends StatefulWidget {
  final int accountId;
  final String ownerName;

  const TransactionListPage({super.key, required this.accountId, required this.ownerName});

  @override
  State<TransactionListPage> createState() => _TransactionListPageState();
}

class _TransactionListPageState extends State<TransactionListPage> {
  final TransactionService _transactionService = TransactionService();
  List<Transaction> _transactions = [];
  bool _isLoading = true;
  String? _errorMessage;

  @override
  void initState() {
    super.initState();
    _loadTransactions();
  }

  Future<void> _loadTransactions() async {
    try {
      final transactions = await _transactionService.getByAccountId(widget.accountId);
      setState(() {
        _transactions = transactions;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _errorMessage = e.toString();
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('${widget.ownerName} - Hareketler')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _errorMessage != null
              ? Center(child: Text(_errorMessage!))
              : _transactions.isEmpty
                  ? const Center(child: Text('Hareket bulunamadı'))
                  : ListView.builder(
                      itemCount: _transactions.length,
                      itemBuilder: (context, index) {
                        final t = _transactions[index];
                        return ListTile(
                          title: Text(t.type),
                          subtitle: Text(t.description),
                          trailing: Text('${t.amount} ₺'),
                          );
                      },
                    ),
    );
  }
}

class _CustomerListPageState extends State<CustomerListPage> {
  final CustomerService _customerService = CustomerService();
  List<Account> _accounts = [];
  bool _isLoading = true;
  String? _errorMessage;

  @override
  void initState() {
    super.initState();
    _loadAccounts();
  }

  Future<void> _loadAccounts() async {
    try {
      final accounts = await _customerService.getAll();
      setState(() {
        _accounts = accounts;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _errorMessage = e.toString();
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
  title: const Text('Hesaplar'),
  actions: [
    IconButton(
      icon: const Icon(Icons.send),
      tooltip: 'Para Transferi',
      onPressed: () {
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => const TransferPage()),
        );
      },
    ),
  ],
),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _errorMessage != null
              ? Center(child: Text(_errorMessage!))
              : ListView.builder(
                  itemCount: _accounts.length,
                  itemBuilder: (context, index) {
                    final account = _accounts[index];
                    return ListTile(
                      title: Text(account.owner),
                      trailing: Text('${account.balance} ₺'),
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => TransactionListPage(
                              accountId: account.id,
                              ownerName: account.owner,
                              ),
                            ),
                          );
                          },
                        );
                  },
                ),
    );
  }
}

class _LoginPageState extends State<LoginPage> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  String? _errorMessage;
  bool _isLoading = false;

  Future<void> _handleLogin() async {
    setState(() {
      _isLoading = true;
      _errorMessage = null;
    });

    final authService = context.read<AuthService>();
    final success = await authService.login(
      _emailController.text,
      _passwordController.text,
    );

    setState(() {
      _isLoading = false;
    });

    if (success) {
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => const CustomerListPage()),
      );
    } else {
      setState(() {
        _errorMessage = 'Giriş başarısız. Email veya şifre hatalı.';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Giriş Yap')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _emailController,
              decoration: const InputDecoration(labelText: 'Email'),
              keyboardType: TextInputType.emailAddress,
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _passwordController,
              decoration: const InputDecoration(labelText: 'Şifre'),
              obscureText: true,
            ),
            const SizedBox(height: 24),
            _isLoading
                ? const CircularProgressIndicator()
                : ElevatedButton(
                    onPressed: _handleLogin,
                    child: const Text('Giriş'),
                  ),
            if (_errorMessage != null) ...[
              const SizedBox(height: 16),
              Text(_errorMessage!, style: const TextStyle(color: Colors.red)),
            ],
          ],
        ),
      ),
    );
  }
}
  class TransferPage extends StatefulWidget {
  const TransferPage({super.key});

  @override
  State<TransferPage> createState() => _TransferPageState();
}

  class _TransferPageState extends State<TransferPage> {
  final TextEditingController _fromController = TextEditingController();
  final TextEditingController _toController = TextEditingController();
  final TextEditingController _amountController = TextEditingController();
  final TransferService _transferService = TransferService();
  String? _message;
  bool _isLoading = false;

  Future<void> _handleTransfer() async {
    setState(() {
      _isLoading = true;
      _message = null;
    });

    final fromId = int.tryParse(_fromController.text);
    final toId = int.tryParse(_toController.text);
    final amount = double.tryParse(_amountController.text);

    if (fromId == null || toId == null || amount == null || amount <= 0) {
      setState(() {
        _message = 'Geçersiz giriş.';
        _isLoading = false;
      });
      return;
    }

    final success = await _transferService.transfer(fromId, toId, amount);

    setState(() {
      _isLoading = false;
      _message = success ? 'Transfer başarılı!' : 'Transfer başarısız.';
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Para Transferi')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _fromController,
              decoration: const InputDecoration(labelText: 'Gönderen Hesap ID'),
              keyboardType: TextInputType.number,
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _toController,
              decoration: const InputDecoration(labelText: 'Alıcı Hesap ID'),
              keyboardType: TextInputType.number,
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _amountController,
              decoration: const InputDecoration(labelText: 'Tutar'),
              keyboardType: TextInputType.number,
            ),
            const SizedBox(height: 24),
            _isLoading
                ? const CircularProgressIndicator()
                : ElevatedButton(
                    onPressed: _handleTransfer,
                    child: const Text('Gönder'),
                  ),
            if (_message != null) ...[
              const SizedBox(height: 16),
              Text(
                _message!,
                style: TextStyle(
                  color: _message == 'Transfer başarılı!' ? Colors.green : Colors.red,
                ),
              ),
            ],
          ],
        ),
      ),
    );
  }
}
