using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //bir servisi burada bir kere kaydediyorsun, sonra ihtiyaç duyan her yere .NET otomatik "enjekte" ediyor.

var app = builder.Build(); //builder'ı gerçek uygulamayı oluşturuyor.

// Configure the HTTP request pipeline.
//Bu, middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//accountları çektiğimiz get endpoint 
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

//belirli bir accountu id ile çektiğimiz get endpoint 
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

//account eklediğimiz post endpoint 
app.MapPost("/api/accounts",(Account newAccount) =>
{
    return Results.Created($"/api/accounts/{newAccount.Id}",newAccount); //REST konvansiyonuna göre "yeni bir kaynak oluşturdum, işte onun adresi" demenin standart yolu bu.
})
.WithName("CreateAccount")
.WithOpenApi();


//accountun balance'ını güncellediğimiz endpoint, put ile

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


//accountu id ile tespit edip sildiğimiz delete endpointi

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

app.Run();

/*
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{ //Record, immutable (değiştirilemez) veri taşıyan tip.
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/
record Account(int Id, string Owner, decimal Balance) {};



