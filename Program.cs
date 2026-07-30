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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

/*
app.MapGet("/weatherforecast", () => //örnek bir endpoint, MapGet-> GET isteğine cevap veren bir route tanımı
{                                   //Controller sınıfı yazmadan direkt endpoint tanımlayabiliyorsun.
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
*/

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

/*
app.MapPost("/api/accounts",(Account newAccount) =>
{
    return Results.Created($"/api/accounts/{newAccount.Id}",newAccount);
})
.WithName("CreateAccount")
.WithOpenApi();
*/

app.MapPost("/api/accounts",(Account newAccount) =>
{
    return Results.Created($"/api/accounts/{newAccount.Id}",newAccount); //REST konvansiyonuna göre "yeni bir kaynak oluşturdum, işte onun adresi" demenin standart yolu bu.
})
.WithName("CreateAccount")
.WithOpenApi();


app.Run();


/*
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{ //Record, immutable (değiştirilemez) veri taşıyan tip.
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/
record Account(int Id, string Owner, decimal Balance)
{
    
};


