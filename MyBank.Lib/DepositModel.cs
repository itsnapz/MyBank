using System;

namespace MyBank.Lib
{
    public class DepositModel
    {
        public Guid BankAccount { get; set; }
        public int MoneyCount { get; set; }
    }
}