using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            var checkingAccount = new Account(Account.AccountType.Checking);
            var savingsAccount = new Account(Account.AccountType.Savings);

            var henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            const string test = "Statement for Henry\r\n\r\nChecking Account\r\n  deposit $100.00\r\nTotal $100.00\r\n\r\n\r\nSavings Account\r\n  deposit $4,000.00\r\n  withdrawal $200.00\r\nTotal $3,800.00\r\n\r\n\r\nTotal In All Accounts $3,900.00\r\n";


            Assert.AreEqual(test, henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            var oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.Savings));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            var oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.AccountType.Savings));
            oscar.OpenAccount(new Account(Account.AccountType.Checking));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
         public void TestThreeAccounts()
        {
            var oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.AccountType.Savings));
            oscar.OpenAccount(new Account(Account.AccountType.Checking));
            oscar.OpenAccount(new Account(Account.AccountType.MaxiSavings));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }




        
    }
}
