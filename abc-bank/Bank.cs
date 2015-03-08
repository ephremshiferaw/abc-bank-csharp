using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Bank
    {
        private readonly List<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {

            if (customer == null)
                throw new ArgumentNullException("customer", "Customer was not provided");
            _customers.Add(customer);
        }

        public String CustomerSummary()
        {
            return _customers.Aggregate("Customer Summary",
                (current, c) =>
                    current + ("\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")"));
        }

        /// <summary>
        ///Make sure correct plural of word is created based on the number passed in:
        ///If number passed in is 1 just return the word otherwise add an 's' at the end
        /// </summary>
        /// <param name="number">constion number</param>
        /// <param name="word">word to format</param>
        /// <returns>formated string</returns>
        private static String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            return _customers.Sum(c => c.TotalInterestEarned());
        }

    }
}
