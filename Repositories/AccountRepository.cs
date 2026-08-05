public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id)
    {
        return _context.Accounts.Find(id);
    }

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void Update(Account account)
    {
        _context.SaveChanges();
    }

    public void Delete(Account account)
    {
        _context.Remove(account);
        _context.SaveChanges();
    }
}