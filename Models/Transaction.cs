public class Transaction
{
    public int Id {get;set;}
    public int AccountId {get;set;}
    public string Type {get;set;} = string.Empty; // "Deposit", "Withdrawal", "Transfer"
    public decimal Amount {get;set;}
    public decimal BalanceAfter {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public string? Description {get;set;}

}