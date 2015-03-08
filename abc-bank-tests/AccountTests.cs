using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank.Tests
{
    [TestClass()]
    public class AccountTests
    {
        [TestMethod()]
        public void AccountTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            Assert.AreEqual(0, checkingAccount.Transactions.Count);

        }

        [TestMethod()]
        public void DepositTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(1000);
            Assert.AreEqual(1, checkingAccount.Transactions.Count);
            Assert.AreEqual(1000, checkingAccount.Balance());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositTestZero()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(0);
         
        }



        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositTestNegative()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(-100);

        }



        [TestMethod()]
        public void WithdrawTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(1000);
            Assert.AreEqual(1, checkingAccount.Transactions.Count);
            checkingAccount.Withdraw(500);
            Assert.AreEqual(2, checkingAccount.Transactions.Count);
            Assert.AreEqual(500, checkingAccount.Balance());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawTestZero()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(1000);
            checkingAccount.Withdraw(0);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawTestNegative()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(1000);
            checkingAccount.Withdraw(-1000);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawTestOverDraw()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(100);
            checkingAccount.Withdraw(1000);

        }

        [TestMethod()]
        public void InterestEarnedTest_checking()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            checkingAccount.Deposit(100);
            var interest = checkingAccount.InterestEarned();
            Assert.AreEqual(0.1, interest);
        }

        [TestMethod()]
        public void InterestEarnedTest_saving()
        {
            var account = new Account(Account.AccountType.Savings);
            account.Deposit(1000);
            var interest = account.InterestEarned();

            Assert.AreEqual(1.0, interest);

            account.Deposit(1000);
            interest = account.InterestEarned();

            Assert.AreEqual(3.0, interest);
        }


        [TestMethod()]
        public void InterestEarnedTest_maxsavingNo10Withdrawl()
        {
            var account = new Account(Account.AccountType.MaxiSavings);
            account.Deposit(1000,DateProvider.Instance.Now.AddDays(-11));
            var interest = account.InterestEarned();
            Assert.AreEqual(50, interest);
            account.Deposit(1000);
            interest = account.InterestEarned();
            Assert.AreEqual(100, interest);
        }


        [TestMethod()]
        public void InterestEarnedTest_maxsaving10Withdrawl()
        {
            var account = new Account(Account.AccountType.MaxiSavings);
            account.Deposit(1000, DateProvider.Instance.Now.AddDays(-11));
            var interest = account.InterestEarned();
            Assert.AreEqual(50, interest);
            account.Withdraw(100);
            account.Deposit(1100);
            interest = account.InterestEarned();
            Assert.AreEqual(2.0, interest);
        }


        [TestMethod()]
        public void BalanceTest()
        {
            var account = new Account(Account.AccountType.Savings);
            account.Deposit(100);
            Assert.AreEqual(100, account.Balance());
        }

        [TestMethod()]
        public void SumTransactionsTest()
        {
            var account = new Account(Account.AccountType.Savings);
            account.Deposit(100);
            account.Withdraw(50);
            Assert.AreEqual(50, account.SumTransactions());
        }

        [TestMethod()]
        public void GetAccountTypeTest()
        {
            var account = new Account(Account.AccountType.Savings);
            Assert.AreEqual(Account.AccountType.Savings, account.GetAccountType());
        }

        [TestMethod()]
        public void CompareToTest()
        {
            var account = new Account(Account.AccountType.Savings);
            var ac1 = account;
            Assert.AreEqual(account, account);

        }

       
        [TestMethod()]
        public void ToStringTest()
        {
            var account = new Account(Account.AccountType.Savings);
            Assert.AreEqual("Savings Account", account.ToString());

            account = new Account(Account.AccountType.Checking);
            Assert.AreEqual("Checking Account", account.ToString());

            account = new Account(Account.AccountType.MaxiSavings);
            Assert.AreEqual("Maxi Savings Account", account.ToString());

            account = new Account(Account.AccountType.MaxiSavingsOrignal);
            Assert.AreEqual("Maxi Savings Account Orignal", account.ToString());
        }
    }
}
