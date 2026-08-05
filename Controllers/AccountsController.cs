using Microsoft.AspNetCore.Mvc;

[ApiController] //Bu bir attribute (C#'ta köşeli parantezle yazılan, sınıf veya metoda ek davranış kazandıran etiketler).
[Route("api/accounts")]

public class AccountsController : ControllerBase
{
    
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        /*
        var accounts = new []
        {
            new Account{Id=1,Owner="ahmet",Balance=1500m},
            new Account{Id=2,Owner="zeynep",Balance=2500m},
            new Account{Id=3,Owner="mehmet",Balance=3500m},
            new Account{Id=4,Owner="elif",Balance=4500m},
            new Account{Id=5,Owner="ali",Balance=500},
        };
        */
        var accounts = _service.GetAllAccounts();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {

        //var account = _context.Accounts.Find(id);//FirstOrDefaulta göre daha hızlı
        var account = _service.GetAccountById(id);

        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPost]
    public IActionResult PostAccount(Account newAccount)
    {
        if (string.IsNullOrWhiteSpace(newAccount.Owner))
            return BadRequest();
        
        if(newAccount.Balance < 0 )
            return BadRequest();

        //_context.Accounts.Add(newAccount); //"bu nesneyi Accounts tablosuna ekle" der, ama henüz veritabanına yazmaz — sadece hafızada işaretler
        //_context.SaveChanges();//değişiklikleri gerçekten veritabanına yazar (INSERT SQL'i çalışır). Bu çağrı olmadan hiçbir şey kaydedilmez

        _service.CreateAccount(newAccount);
        

        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id},newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult PutAction(int id, Account newAccount)
    {
        /*
        var account = _service.GetAccountById(id);
        if (account == null)
            return NotFound();
        
        //var updatedAccount = account with {Owner = newAccount.Owner , Balance = newAccount.Balance};

        account.Owner = newAccount.Owner;
        account.Balance = newAccount.Balance;
        */

        //updateAccount içinde yukardaki kontroller olduğu için sadeleştirme yapıldı.

        var account = _service.UpdateAccount(id,newAccount);
        if (account == null)
            return NotFound();

        return Ok(account);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchAction(int id, decimal balance)
    {
        if (balance <= 0)
            return BadRequest();

        var account = _service.Deposit(id,balance);
        if (account==null)
            return NotFound();
        
        return Ok(account);
    }   

    [HttpDelete("{id}")]
    public IActionResult DeleteAction(int id)
    {
        /*
        var account = _service.GetAccountById(id);
        if(account == null)
            return NotFound();
        */
        /*
        _context.Accounts.Remove(account);
        _context.SaveChanges();
        */

        var result = _service.DeleteAccount(id);
        if (result == false)
            return NotFound();
        
        return NoContent();
    }

}
