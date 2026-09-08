public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public List<Account> GetAllAccounts()
    {
        return _repository.GetAll();
    }

    public Account? GetAccountById(int id)
    {
        return _repository.GetById(id);
    }

    public Account CreateAccount(Account account)
    {
        _repository.Add(account);
        return account;
    }

    public Account? UpdateAccount(int id, Account account)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
            return null;

        existing.Owner = account.Owner;
        existing.Balance = account.Balance;
        _repository.Update(existing);
        return existing;
    }

    public Account? Deposit(int id, decimal amount)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
            return null;
        
        existing.Balance += amount;
        _repository.Update(existing);
        return existing;
    }

    public bool DeleteAccount(int id)
    {
        var account = _repository.GetById(id);
        if (account == null)
            return false;

        _repository.Delete(account);
        return true;

    }

    public bool Transfer(int fromId, int toId, decimal amount)
{
    var from = _repository.GetById(fromId);
    var to = _repository.GetById(toId);

    if (from == null || to == null) return false;
    if (from.Balance < amount) return false;

    from.Balance -= amount;
    to.Balance += amount;
    _repository.Update(from);
    _repository.Update(to);
    return true;
}

    









}