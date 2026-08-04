using Microsoft.AspNetCore.Mvc;

[ApiController] //Bu bir attribute (C#'ta köşeli parantezle yazılan, sınıf veya metoda ek davranış kazandıran etiketler).
[Route("api/accounts")]

public class AccountsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var accounts = new []
        {
            new Account(Id:1,Owner:"ahmet",Balance:1500m),
            new Account(Id:2,Owner:"zeynep",Balance:2500m),
            new Account(Id:3,Owner:"mehmet",Balance:3500m),
            new Account(Id:4,Owner:"elif",Balance:4500m),
            new Account(Id:5,Owner:"ali",Balance:500),
        };
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        var accounts = new []
        {
            new Account(Id:1,Owner:"ahmet",Balance:1500m),
            new Account(Id:2,Owner:"zeynep",Balance:2500m),
            new Account(Id:3,Owner:"mehmet",Balance:3500m),
            new Account(Id:4,Owner:"elif",Balance:4500m),
            new Account(Id:5,Owner:"ali",Balance:500),
        };

        var account = accounts.FirstOrDefault(a=> a.Id == id);

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

        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id},newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult PutAction(int id, Account newAccount)
    {
        var accounts = new []
        {
            new Account(Id:1,Owner:"ahmet",Balance:1500m),
            new Account(Id:2,Owner:"zeynep",Balance:2500m),
            new Account(Id:3,Owner:"mehmet",Balance:3500m),
            new Account(Id:4,Owner:"elif",Balance:4500m),
            new Account(Id:5,Owner:"ali",Balance:500),
        };

        var account = accounts.FirstOrDefault(a=> a.Id == id);
        if (account == null)
            return NotFound();
        
        var updatedAccount = account with {Owner = newAccount.Owner , Balance = newAccount.Balance};
        return Ok(updatedAccount);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchAction(int id, decimal balance)
    {
        var accounts = new []
        {
            new Account(Id:1,Owner:"ahmet",Balance:1500m),
            new Account(Id:2,Owner:"zeynep",Balance:2500m),
            new Account(Id:3,Owner:"mehmet",Balance:3500m),
            new Account(Id:4,Owner:"elif",Balance:4500m),
            new Account(Id:5,Owner:"ali",Balance:500),
        };

        var account = accounts.FirstOrDefault(a=> a.Id == id);
        if (account == null)
            return NotFound();

        if (balance <= 0 )
            return BadRequest();

        var updated = account with {Balance = account.Balance + balance};
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAction(int id)
    {
        var accounts = new []
        {
            new Account(Id:1,Owner:"ahmet",Balance:1500m),
            new Account(Id:2,Owner:"zeynep",Balance:2500m),
            new Account(Id:3,Owner:"mehmet",Balance:3500m),
            new Account(Id:4,Owner:"elif",Balance:4500m),
        };

        var account = accounts.FirstOrDefault(a=>a.Id == id);
        if(account == null)
            return NotFound();

        return NoContent();
    }

}
