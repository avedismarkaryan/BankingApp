# Banking App — Full Stack Project

.NET REST API, Angular Admin Panel ve Flutter Mobil Uygulama ile geliştirilmiş bir bankacılık uygulaması.

## Proje Mimarisi

```
Flutter (Mobil) ──→ REST API ──→ Service Layer ──→ Repository Layer ──→ SQLite Database
Angular (Admin) ──↗
```

## Teknolojiler

| Katman | Teknoloji |
|--------|-----------|
| Backend API | ASP.NET Core 8, Entity Framework Core, SQLite |
| Authentication | JWT (JSON Web Token) |
| Admin Panel | Angular 21, TypeScript, RxJS |
| Mobil Uygulama | Flutter 3.35, Dart, Provider |
| Test | xUnit, Moq |
| Dokümantasyon | Swagger, Postman Collection |

## Proje Yapısı

```
BankingApi/
├── Controllers/          # API endpoint'leri (Accounts, Auth, Transactions, Features)
├── Service/              # İş mantığı katmanı
├── Repositories/         # Veritabanı işlemleri katmanı
├── Interfaces/           # Servis ve repository sözleşmeleri
├── Models/               # Veri modelleri (Account, User, Transaction, Feature)
├── Data/                 # DbContext (EF Core veritabanı bağlantısı)
├── Migrations/           # Veritabanı şema geçmişi
├── Program.cs            # Uygulama giriş noktası ve middleware pipeline
├── appsettings.json      # JWT ve veritabanı yapılandırması
│
├── banking-admin/        # Angular Admin Panel
│   └── src/app/
│       ├── services/     # AuthService, CustomerService, FeatureService
│       ├── interceptors/ # JWT token otomatik ekleme (HTTP interceptor)
│       ├── customer-list/        # Müşteri listesi component'i
│       ├── login/                # Giriş ekranı component'i
│       └── feature-management/   # Feature toggle yönetim ekranı
│
├── banking_mobile/       # Flutter Mobil Uygulama
│   └── lib/
│       ├── services/     # AuthService, CustomerService, TransactionService, TransferService
│       └── main.dart     # Tüm ekranlar (Login, Hesaplar, Hareketler, Transfer)
│
└── BankingApi.Tests/     # Unit testler (xUnit + Moq)
    └── AccountServiceTests.cs  # AccountService için 6 test
```

## API Endpoint'leri

### Auth (Kimlik Doğrulama)
| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| POST | `/api/auth/register` | Yeni kullanıcı kaydı |
| POST | `/api/auth/login` | Giriş yap, JWT token al |

### Accounts (Hesaplar) — JWT gerekli
| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/accounts` | Tüm hesapları listele |
| GET | `/api/accounts/{id}` | Belirli hesabı getir |
| POST | `/api/accounts` | Yeni hesap oluştur |
| PUT | `/api/accounts/{id}` | Hesabı güncelle |
| PATCH | `/api/accounts/{id}` | Bakiye ekle (deposit) |
| DELETE | `/api/accounts/{id}` | Hesabı sil |
| POST | `/api/accounts/transfer` | Hesaplar arası transfer |

### Transactions (Hareketler) — JWT gerekli
| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/transactions` | Tüm hareketleri listele |
| GET | `/api/transactions/account/{accountId}` | Hesaba ait hareketleri getir |

### Features (Özellik Yönetimi)
| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/features` | Tüm feature'ları listele |
| GET | `/api/features/{id}` | Belirli feature'ı getir |
| POST | `/api/features` | Yeni feature ekle |
| PATCH | `/api/features/{id}` | Feature'ı aç/kapat |

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8 SDK
- Node.js (v18+) ve Angular CLI
- Flutter SDK (3.x)

### 1. API (Backend)
```bash
cd BankingApi
dotnet ef database update    # Veritabanını oluştur
dotnet run                   # API: http://localhost:5036
```
Swagger arayüzü: `http://localhost:5036/swagger`

### 2. Angular Admin Panel
```bash
cd banking-admin
npm install                  # Bağımlılıkları yükle (ilk seferde)
ng serve                     # Admin Panel: http://localhost:4200
```

### 3. Flutter Mobil Uygulama
```bash
cd banking_mobile
flutter pub get              # Bağımlılıkları yükle (ilk seferde)
open -a Simulator            # iOS Simulator'ı aç
flutter run                  # Uygulamayı başlat
```

> Her üç uygulama aynı anda çalışmalıdır — Angular ve Flutter, API'ye HTTP istekleri atar.

## Özellikler

### Backend (API)
- Katmanlı mimari: Controller → Service → Repository → Database
- JWT ile kimlik doğrulama ve yetkilendirme
- Entity Framework Core ile SQLite veritabanı yönetimi
- CORS yapılandırması (Angular erişimi için)
- Swagger ile otomatik API dokümantasyonu
- Postman Collection ile test desteği

### Angular Admin Panel
- JWT ile giriş yapma ve token yönetimi (HTTP interceptor)
- Müşteri listesi (API'den gerçek veri)
- Feature toggle yönetim ekranı (deploy olmadan menü öğelerini aç/kapat)
- Koşullu navbar render (feature durumuna göre)

### Flutter Mobil Uygulama
- JWT ile giriş ve token saklama (SharedPreferences)
- Hesap listesi ekranı
- Hesap hareketleri ekranı (hesaba tıklayınca detay)
- Hesaplar arası para transferi ekranı
- Provider ile state yönetimi

### Test
- xUnit + Moq ile 6 unit test (AccountService katmanı)
- Gerçek veritabanına bağımlı olmayan izole testler

## Ekran Görüntüleri

### Angular Admin Panel
- Giriş ekranı → Müşteri listesi → Feature toggle yönetimi

### Flutter Mobil
- Giriş ekranı → Hesap listesi → Hesap hareketleri → Para transferi
