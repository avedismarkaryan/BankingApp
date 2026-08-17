using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext //DbContext'ten miras almak => bu sınıf veritabanına açılan kapı
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    {
    } //Constructor → DbContextOptions alıyor, bunu Program.cs'ten SQLite bağlantısını geçirmek için kullanacağız
    public DbSet<Account> Accounts {get;set;} //veritabanındaki Accounts tablosunu temsil eder.
                                              //DbSet üzerinden Add, Remove, Find, Where gibi işlemler yaparsın — EF Core bunları SQL sorgularına çevirir
    public DbSet<User> Users {get;set;}

    public DbSet<Transaction> Transactions {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasData(
            //HasData: migration çalışınca bu verileri tabloya INSERT eder. 
            //Seed data'da Id'yi elle belirtiyorsun çünkü EF Core bunu bilmeli (auto-increment olsa bile seed'de zorunlu).
            new Transaction { Id = 1, AccountId = 1, Type = "Deposit", Amount = 5000m, BalanceAfter = 5000m, CreatedAt = new DateTime(2026, 1, 15), Description = "Maaş" },
            new Transaction { Id = 2, AccountId = 1, Type = "Withdrawal", Amount = 500m, BalanceAfter = 4500m, CreatedAt = new DateTime(2026, 1, 20), Description = "ATM çekim" },
            new Transaction { Id = 3, AccountId = 1, Type = "Withdrawal", Amount = 1200m, BalanceAfter = 3300m, CreatedAt = new DateTime(2026, 2, 1), Description = "Kira" },
            new Transaction { Id = 4, AccountId = 2, Type = "Deposit", Amount = 8000m, BalanceAfter = 8000m, CreatedAt = new DateTime(2026, 1, 15), Description = "Maaş" },
            new Transaction { Id = 5, AccountId = 2, Type = "Withdrawal", Amount = 200m, BalanceAfter = 7800m, CreatedAt = new DateTime(2026, 1, 22), Description = "Market" },
            new Transaction { Id = 6, AccountId = 2, Type = "Transfer", Amount = 3000m, BalanceAfter = 4800m, CreatedAt = new DateTime(2026, 2, 5), Description = "Hesap transferi"}
        );

    }

    public DbSet<Feature> Features {get;set;}

}
