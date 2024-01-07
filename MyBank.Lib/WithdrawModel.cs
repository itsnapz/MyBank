using System;

namespace MyBank.Lib
{
    public class WithdrawModel
    {
        public Guid BankAccount { get; set; }
        public int MoneyCount { get; set; }
    }
}