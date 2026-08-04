using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext //DbContext'ten miras almak => bu sınıf veritabanına açılan kapı
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    {
    } //Constructor → DbContextOptions alıyor, bunu Program.cs'ten SQLite bağlantısını geçirmek için kullanacağız
    public DbSet<Account> Accounts {get;set;} //veritabanındaki Accounts tablosunu temsil eder.
                                              //DbSet üzerinden Add, Remove, Find, Where gibi işlemler yaparsın — EF Core bunları SQL sorgularına çevirir
}
