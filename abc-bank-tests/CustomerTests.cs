using System;
using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass()]
    public class CustomerTests
    {
        [TestMethod()]
        public void CustomerTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem").OpenAccount(checkingAccount);
            checkingAccount.Deposit(100);
            var statements = ephrem.GetStatement();
            const string test = "Statement for Ephrem\r\n\r\nChecking Account\r\n  deposit $100.00\r\nTotal $100.00\r\n\r\n\r\nTotal In All Accounts $100.00\r\n";
            Assert.AreEqual(test, statements);
        }

        [TestMethod()]
        public void GetNameTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem").OpenAccount(checkingAccount);
            const string test = "Ephrem";
            Assert.AreEqual(test, ephrem.GetName());
        }

        [TestMethod()]
        public void OpenAccountTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem").OpenAccount(checkingAccount);
            checkingAccount.Deposit(100);
            checkingAccount.Withdraw(10);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(90.0, balance);
        }

        [TestMethod()]
        [ExpectedException(typeof (ArgumentException))]
        public void OpenAccountTest_alradyOpend()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem").OpenAccount(checkingAccount);
            ephrem.OpenAccount(checkingAccount);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OpenAccountTest_Null()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(null);

        }





        [TestMethod()]
        public void TransferTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);

            ephrem.Transfer(checkingAccount, savingsAccount, 500);

            balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);

            Assert.AreEqual(500, checkingAccount.Balance());
            Assert.AreEqual(500, savingsAccount.Balance());
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransferNullFromAccount()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);

            ephrem.Transfer(null, savingsAccount, 500);

         
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransferNullToAccount()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);
            ephrem.Transfer(checkingAccount, null, 500);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferSameAccount()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);
            ephrem.Transfer(checkingAccount, checkingAccount, 500);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferExternalAccount()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);
            ephrem.Transfer(checkingAccount, savingsAccount, 500);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferZero()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);
            ephrem.Transfer(checkingAccount, savingsAccount, 0);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferOverDraft()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);
            checkingAccount.Deposit(1000);
            var balance = ephrem.TotalBalance();
            Assert.AreEqual(1000, balance);
            ephrem.Transfer(checkingAccount, savingsAccount, 2000);
        }

        [TestMethod()]
        public void GetNumberOfAccountsTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(checkingAccount);
            ephrem.OpenAccount(savingsAccount);

             var test = 2;
            Assert.AreEqual(test, ephrem.GetNumberOfAccounts());


            ephrem.OpenAccount(new Account(Account.AccountType.Savings));
            test = 3;
            Assert.AreEqual(test, ephrem.GetNumberOfAccounts());
        }

        [TestMethod()]
        public void TotalInterestEarnedTest()
        {
           var savingsAccount = new Account(Account.AccountType.Savings);
            var ephrem = new Customer("Ephrem");
            ephrem.OpenAccount(savingsAccount);

            savingsAccount.Deposit(100);


            var savingsInterest = savingsAccount.InterestEarned();
            Assert.AreEqual(savingsInterest, ephrem.TotalInterestEarned());

            var maxiSavings = new Account(Account.AccountType.MaxiSavings);
            ephrem.OpenAccount(maxiSavings);
            maxiSavings.Deposit(1000,DateTime.Today.AddDays(-15));
            var maxiSavingsIntrest = maxiSavings.InterestEarned();
            Assert.AreEqual(savingsInterest + maxiSavingsIntrest, ephrem.TotalInterestEarned());

        }


       
        [TestMethod()]
        public void GetStatementTest()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var ephrem = new Customer("Ephrem").OpenAccount(checkingAccount);
            checkingAccount.Deposit(100);
            var statements = ephrem.GetStatement();
            const string test = "Statement for Ephrem\r\n\r\nChecking Account\r\n  deposit $100.00\r\nTotal $100.00\r\n\r\n\r\nTotal In All Accounts $100.00\r\n";
            Assert.AreEqual(test, statements);
        }
    }
}
