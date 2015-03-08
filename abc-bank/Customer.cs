using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
    public class Customer
    {
        public string Name { get; private set; }
        private readonly List<Account> _accounts;

        public Customer(String name)
        {
            Name = name;
            _accounts = new List<Account>();
        }

        public String GetName()
        {
            return Name;
        }

        public Customer OpenAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account", "account was not provided");

            if (_accounts.Contains(account))
                throw new ArgumentException("account already opened");
            _accounts.Add(account);
            return this;
        }


        public bool Transfer(Account accountFrom, Account accountTo, double transferAmount, DateTime? transactionDate = null)
        {
            if (accountFrom == null)
                throw new ArgumentNullException("accountFrom", "account was not provided");

            if (accountTo == null)
                throw new ArgumentNullException("accountTo", "account was not provided");

            if (accountFrom == accountTo)
                throw new ArgumentException("transfer to the same account is not allowed");


            if (_accounts.Contains(accountFrom)==false)
                throw new ArgumentException("transfer from an external account is not allowed");


            if (_accounts.Contains(accountTo) == false)
                throw new ArgumentException("transfer to an external account is not allowed");


            if (transferAmount <= 0)
            {
                throw new ArgumentException("transfer amount must be greater than zero");
            }

            var fromBalance = accountFrom.Balance();

            if (fromBalance <= transferAmount)
            {
                throw new ArgumentException("transfer amount must be less than account balance");
            }

            accountFrom.Withdraw(transferAmount, transactionDate);
            accountTo.Deposit(transferAmount, transactionDate);

            return true;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public double TotalInterestEarned()
        {
            return _accounts.Sum(a => a.InterestEarned());
        }

        public double TotalBalance()
        {
            return _accounts.Sum(a => a.Balance());
        }

        public String GetStatement()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Statement for " + Name);
            var total = 0.0;
            foreach (var a in _accounts)
            {
                sb.AppendLine("");
                sb.AppendLine(StatementForAccount(a));
                total += a.SumTransactions();
            }
            sb.AppendLine("");
            sb.AppendLine("Total In All Accounts " + ToDollars(total));
            return sb.ToString();
        }

        private static String StatementForAccount(Account a) 
        {
            var sb = new StringBuilder();
            sb.AppendLine(a.ToString());

            //Now total up all the transactions
            var total = 0.0;
            foreach (var t in a.Transactions)
            {
                sb.AppendLine("  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount));               
                total += t.Amount;
            }
            sb.AppendLine("Total " + ToDollars(total));

            return sb.ToString();
        }

        private static String ToDollars(double d)
        {

            return Math.Abs(d).ToString("C2");
            //d.ToString()
            return d.ToString("$%,.2f");
        }
    }
}
