using System;
using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            var bank = new Bank();
            var john = new Customer("John");
            john.OpenAccount(new Account(Account.AccountType.Checking));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.AccountType.Checking);
            var bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            var bank = new Bank();
            var checkingAccount = new Account(Account.AccountType.MaxiSavingsOrignal);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }



        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestTicksPerMillisecond()
        {
            var bank = new Bank();
            bank.AddCustomer(null);
        }




        [TestMethod]
        public void Null_TransactionDateTest()
        {
            var bank = new Bank();
            var checkingAccount = new Account(Account.AccountType.Checking);
            bank.AddCustomer(new Customer("Ephrem").OpenAccount(checkingAccount));

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), DOUBLE_DELTA);

            checkingAccount.Withdraw(100);

            Assert.AreEqual(0.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
