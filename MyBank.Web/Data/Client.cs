namespace MyBank.Web.Data;

public class Client
{
    public Guid ClientId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int Money { get; set; }
    public int BankCode { get; set; }
    public bool? IsAdmin { get; set; }

    public long? CardId { get; set; }
    public virtual Card Card { get; set; }
}