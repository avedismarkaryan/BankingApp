using System.Buffers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Token girin"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
}); //bir servisi burada bir kere kaydediyorsun, sonra ihtiyaç duyan her yere .NET otomatik "enjekte" ediyor.
builder.Services.AddControllers(); //controllerları tanımladığımız metot. controller'ları DI container'a kaydeder,
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=banking.db"));
builder.Services.AddScoped<IAccountRepository,AccountRepository>(); //IAccountRepository isteyene AccountRepository ver.
builder.Services.AddScoped<IAccountService,AccountService>(); //IAccountService isteyene AccountService ver.
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


var app = builder.Build(); //builder'ı gerçek uygulamayı oluşturuyor.

// Configure the HTTP request pipeline.
//Bu, middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



//accountları çektiğimiz get endpoint
/* 
app.MapGet("/api/accounts", () => //örnek bir endpoint, MapGet-> GET isteğine cevap veren bir route tanımı
{                                   //Controller sınıfı yazmadan direkt endpoint tanımlayabiliyorsun.

    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
    };
    return accounts;
})
.WithName("GetAccount")
.WithOpenApi();
*/

//belirli bir accountu id ile çektiğimiz get endpoint 
/*
app.MapGet("/api/accounts/{id}",(int id) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
    };
    
    var account = accounts.FirstOrDefault(a => a.Id == id); //şunu sor.

    return account is not null ? Results.Ok(account) : Results.NotFound();

})
.WithName("GetAccountById")
.WithOpenApi();
*/
//account eklediğimiz post endpoint 
/*
app.MapPost("/api/accounts",(Account newAccount) =>
{
    if (string.IsNullOrWhiteSpace(newAccount.Owner)) //string metodunun boş gelme ihtimaline karşı aldığımız aksiyon.
        return Results.BadRequest("there has to be an account name");
    
    if (newAccount.Balance < 0)
        return Results.BadRequest("the balance should be more than 0");

    return Results.Created($"/api/accounts/{newAccount.Id}",newAccount);
    //return Results.Created($"/api/accounts/{newAccount.Id}",newAccount); //REST konvansiyonuna göre "yeni bir kaynak oluşturdum, işte onun adresi" demenin standart yolu bu.
})
.WithName("CreateAccount")
.WithOpenApi();
*/



//accountun balance'ını güncellediğimiz endpoint, put ile
/*
app.MapPut("/api/accounts/{id}", (int id, Account updatedAccount) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
    };
    var account = accounts.FirstOrDefault(a => a.Id == id);

    if (account == null)
    {
        return Results.NotFound();
    }
    var updated = account with {Balance = updatedAccount.Balance};
    return Results.Ok(updated);
})
.WithName("UpdateAccount")
.WithOpenApi();
*/

//accountu id ile tespit edip sildiğimiz delete endpointi
/*
app.MapDelete("/api/accounts/{id}",(int id) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
    };

    var account = accounts.FirstOrDefault(a=> a.Id == id);
    if (account == null)
    {
        return Results.NotFound();
    }
    
    return Results.NoContent();
    

})
.WithName("DeleteAccountById")
.WithOpenApi();
*/

//Query String ile filtreleme yaptığımız get endpointi
/*
app.MapGet("/api/accounts",(decimal? minBalance , string? owner) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
    };
    IEnumerable<Account> result = accounts;

    if(minBalance.HasValue)
    {
        result = result.Where(a=> a.Balance >= minBalance.Value);
    }
    if(owner != null)
    {
        result = result.Where(a=> a.Owner == owner);
        //result = result.Where(a=> a.Owner.Equals(owner, StringComparison.OrdinalIgnoreCase)); -Büyük küçük harf duyarlılığını kaldırmak için-
    }
    return Results.Ok(result);

})
.WithDisplayName("GetAccountByCondition")
.WithOpenApi();
*/

//Belirli bir bakiyenin altındaki hesapları getir
/*
app.MapGet("/api/accounts/low-balance",(decimal? threshold) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
        new Account(Id:5,Owner:"ali",Balance:500),
    };

    //IEnumerable<Account> result = accounts; buna gerek yok, çünkü zincirleme bir kontrol yapmıyoruz.
    //üstteki örnekte zincirleme iki kontrol olduğu için değişkene gerek vardı ama burada kullanmasak da olur.

    var limit = threshold ?? 1000m; //bu satırda sol tarafın (threshold) değeri yoksa sağ tarafı kullan diyoruz.
    var result = accounts.Where(a=> a.Balance < limit);
    return Results.Ok(result);
    
})
.WithDisplayName("GetAccountsWithQuery")
.WithOpenApi();
*/

//Hesap bakiyesine para ekle
/*
app.MapPatch("/api/accounts/{id}/deposit",(int id ,decimal amount) =>
{
    var accounts = new []
    {
        new Account(Id:1,Owner:"ahmet",Balance:1500m),
        new Account(Id:2,Owner:"zeynep",Balance:2500m),
        new Account(Id:3,Owner:"mehmet",Balance:3500m),
        new Account(Id:4,Owner:"elif",Balance:4500m),
        new Account(Id:5,Owner:"ali",Balance:500),
    };

    if (amount<=0)
        return Results.BadRequest(); //amountu kontrol ettik ve yanlışsa 400 döndük.
    
    var account = accounts.FirstOrDefault(a=> a.Id == id); //idye göre hesap var mı diye kontrol ettik.
    
    if (account == null)    
        return Results.NotFound(); //hesap yoksa 404 döndük.
    
    var newBalance = account.Balance + amount; //bakiyeyi güncelledik.
    var updated = account with {Balance = newBalance}; //var olan hesabın bakiyesine atadık.

    return Results.Ok(updated);
        
})
.WithDisplayName("PatchAccount")
.WithOpenApi();
*/
app.MapControllers(); //ikincisi gelen HTTP isteklerini controller'lara yönlendirir.
app.Run();


//record Account(int Id, string Owner, decimal Balance) {};



