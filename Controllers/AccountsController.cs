using Microsoft.AspNetCore.Mvc;

[ApiController] //Bu bir attribute (C#'ta köşeli parantezle yazılan, sınıf veya metoda ek davranış kazandıran etiketler).
[Route("api/accounts")]

public class AccountsController : ControllerBase
{
    
    private readonly AppDbContext _context;

    public AccountsController(AppDbContext context)
    {
        _context = context;
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
        var accounts = _context.Accounts.ToList();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {

        var account = _context.Accounts.Find(id);//FirstOrDefaulta göre daha hızlı

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

        _context.Accounts.Add(newAccount); //"bu nesneyi Accounts tablosuna ekle" der, ama henüz veritabanına yazmaz — sadece hafızada işaretler
        _context.SaveChanges();//değişiklikleri gerçekten veritabanına yazar (INSERT SQL'i çalışır). Bu çağrı olmadan hiçbir şey kaydedilmez

        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id},newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult PutAction(int id, Account newAccount)
    {
        
        var account = _context.Accounts.Find(id);
        if (account == null)
            return NotFound();
        
        //var updatedAccount = account with {Owner = newAccount.Owner , Balance = newAccount.Balance};
        account.Owner = newAccount.Owner;
        account.Balance = newAccount.Balance;
        _context.SaveChanges();

        return Ok(account);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchAction(int id, decimal balance)
    {
        
        var account = _context.Accounts.Find(id);
        if (account == null)
            return NotFound();

        if (balance <= 0 )
            return BadRequest();

        account.Balance = account.Balance + balance;
        _context.SaveChanges();

        return Ok(account);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAction(int id)
    {
        
        var account = _context.Accounts.Find(id);
        if(account == null)
            return NotFound();

        _context.Accounts.Remove(account);
        _context.SaveChanges();
        
        return NoContent();
    }

}
