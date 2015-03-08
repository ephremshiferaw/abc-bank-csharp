using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace abc_bank
{
    public class Account:IComparable<Account>
    {

        public enum AccountType
        {
            [Description("Checking Account")] Checking = 0,

            [Description("Savings Account")] Savings = 1,

            [Description("Maxi Savings Account Orignal")] MaxiSavingsOrignal = 2,

            [Description("Maxi Savings Account")] MaxiSavings = 3
        }

        private readonly AccountType _accountType;
        private readonly List<Transaction> _transactions;

        /// <summary>
        /// No One out side should be able to add transactions with out calling the methods
        /// </summary>
        public ReadOnlyCollection<Transaction> Transactions
        {
            get { return _transactions.AsReadOnly(); }
        }

        public Guid AccountId { get; private set; }

        public Account(AccountType accountType) 
        {
            _accountType = accountType;
            _transactions = new List<Transaction>();
            AccountId = Guid.NewGuid();//User this to unquily id an account
        }

     
        /// <summary>
        /// Overload needed to capture Transaction date
        /// No requirement yet to Validate date, except of Null
        /// TODO: What are the Date Validation Rules??
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transactionDate"></param>
        public void Deposit(double amount, DateTime? transactionDate=null)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            _transactions.Add(transactionDate == null
                ? new Transaction(amount,Transaction.TransactionTypes.Deposit)
                : new Transaction(amount,transactionDate.Value,Transaction.TransactionTypes.Deposit));
        }


        /// <summary>
        /// Overload needed to capture Transaction date
        /// No requirement yet to Validate date, except of Null
        /// TODO: What are the Date Validation Rules??
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transactionDate"></param>
        public void Withdraw(double amount, DateTime? transactionDate = null)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }

             if (Balance() < amount) {
                throw new ArgumentException("amount must not be more than balance");
            }
          
           
            _transactions.Add(transactionDate == null
              ? new Transaction(-amount, Transaction.TransactionTypes.Withdraw)
              : new Transaction(-amount, transactionDate.Value, Transaction.TransactionTypes.Withdraw));
        }





        public double InterestEarned()
        {
            var amount = SumTransactions();
            switch (_accountType)
            {
                case AccountType.Savings:
                    if (amount <= 1000)
                        return amount*0.001;
                    return 1 + (amount - 1000)*0.002;
                case AccountType.MaxiSavingsOrignal:
                    if (amount <= 1000)
                        return amount*0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000)*0.05;
                    return 70 + (amount - 2000)*0.1;
                case AccountType.MaxiSavings:
                    if (HasNoWithdrawalsInDays(10)) //5% assuming no withdrawals in the past 10 days 
                        return amount * 0.05;
                    return amount * 0.001; //otherwise 0.1%
                default:
                    return amount*0.001;
            }
        }


        private bool HasNoWithdrawalsInDays(int days)
        {
            var a = from transaction in _transactions
                    where DateProvider.Instance.DaysSince(transaction.TransactionDate) <= days && transaction.TransactionType == Transaction.TransactionTypes.Withdraw
                select transaction;
            if (a.ToList().Count > 0) return false;
                    return true;
        }

        public double Balance()
        {
            return SumTransactions();
        }


        public double SumTransactions() {
            return _transactions.Sum(t => t.Amount);
        }

     
        public AccountType GetAccountType() 
        {
            return _accountType;
        }


        public int CompareTo(Account other)
        {
            return AccountId == other.AccountId ? 1 : 0;
        }

        public override string ToString()
        {
            var fi = _accountType.GetType().GetField(_accountType.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : _accountType.ToString();
        }
    }
}
