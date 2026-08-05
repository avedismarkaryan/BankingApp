public interface IAccountRepository
{
    List<Account> GetAll();
    Account? GetById(int id);
    //yukardaki GetAll ve GetById isimlerini kafamıza göre verdik. Ctrl'deki ile tutarlı olmak zorunda değil.
    void Add(Account account);
    void Update(Account account);
    void Delete(Account account);


}