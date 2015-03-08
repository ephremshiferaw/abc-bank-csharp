using System;
namespace abc_bank
{
    public class Transaction
    {
        public enum TransactionTypes
        {
            Deposit,
            Withdraw
        }

        public TransactionTypes TransactionType { get; private set; }
        public double Amount { get; private set; }

        public DateTime TransactionDate{ get; private set; }

        public Transaction(double amount, TransactionTypes transactionType)
            : this(amount, DateProvider.Instance.Now, transactionType)
        {
        }

        public Transaction(double amount, DateTime transactionDate, TransactionTypes transactionType)
        {
            Amount = amount;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
        }
    }
}
