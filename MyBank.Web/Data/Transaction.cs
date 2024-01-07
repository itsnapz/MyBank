namespace MyBank.Web.Data;

public class Transaction
{
    public int TransactionId { get; set; }
    public Guid SenderId { get; set; }
    public Guid TargetId { get; set; }
    public int SenderBankCode { get; set; }
    public int TargetBankCode { get; set; }
    public int MoneyCount { get; set; }
    public int? VariableSymbol { get; set; }
    public string Date { get; set; }
    public bool? Sent { get; set; }
}