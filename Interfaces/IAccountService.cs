public interface IAccountService
{
    List<Account> GetAllAccounts();
    Account? GetAccountById(int id);
    Account CreateAccount(Account account);
    Account? UpdateAccount(int id, Account account);
    Account? Deposit(int id, decimal amount);
    bool DeleteAccount(int id);
    bool Transfer(int fromId, int toId, decimal amount);



}