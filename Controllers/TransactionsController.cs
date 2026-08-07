using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/transactions")]
[Authorize]

public class TransactionController : ControllerBase
{
    private readonly AppDbContext _context;
    public TransactionController (AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var transactions = _context.Transactions.ToList();
        return Ok(transactions);
    }

    [HttpGet("account/{accountId}")]
    public IActionResult GetById(int accountId)
    {
        var transactions = _context.Transactions
            .Where(t=> t.AccountId == accountId)
            .ToList();

        return Ok(transactions);
        
    }
}