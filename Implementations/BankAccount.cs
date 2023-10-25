using MiniBankApp2.Enums;
using MiniBankApp2.Interfaces;
using MiniBankApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankApp2.Implementations
{
    internal class BankAccount : IBankAccount
    {
        // These are fields that hold pieces of data/state, and are accessible throughout the class but not outside.
        private readonly string _firstName;
        private readonly string _lastName;
        private double _balance;
        private readonly AccountType _accountType;
        private readonly int _accountNumber;

        private List<Transaction> _transactions = new List<Transaction>();
        private List<Beneficiary> _beneficiaries = new List<Beneficiary>();

        private const int _accountNumberSeed = 1000000000;
        private const string _bankName = "KONYEFA BANK";

        // These are properties that make pieces of data/state available to external classes
        public string AccountName => $"{_firstName} {_lastName}".ToUpper();
        public int AccountNumber => _accountNumber;
        public string AccountType => _accountType.ToString().ToUpper();
        public string Bank => _bankName;

        // Define a method for opening account. We can use the constructor for this purpose
        public BankAccount(string firstName, string LastName, double initialBalance, AccountType accountType)
        {
            _firstName = firstName;
            _lastName = LastName;
            _balance = initialBalance;
            _accountType = accountType;
            _accountNumber = _accountNumberSeed + 1;
            TrackTransaction(TransactionType.Deposit, initialBalance, "Initial Deposit");
        }

        public List<Transaction> Transactions => _transactions;

        public List<Beneficiary> Beneficiaries => _beneficiaries;

        // Define a method for depositing funds
        public void Deposit(double amount)
        {

            //validate the amount
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount! Kindly ensure that 'Amount' is greater than zero.");
                return;
            }

            // Add the deposited amount for withdrawing funds
            _balance += amount;

            TrackTransaction(TransactionType.Deposit, amount, "Cash Deposit");

            //Display the new balance
            DisplayBalance();

        }

        // Define a method for withdrawing funds
        public void Withdraw(double amount)
        {
            //Validate the amount
            if (amount > _balance)
            {
                Console.WriteLine("Insufficient funds! Kindly enter an amount not greater than your current balance.");
                return;
            }

            //Deduct the withdrawn amount from the balance
            _balance -= amount;

            TrackTransaction(TransactionType.Withdrawal, amount, "Cash Withdrawal");

            //Display the new balance
            DisplayBalance();
        }

        // Define a method for displaying funds
        public void DisplayBalance()
        {
            Console.WriteLine($"Your current balance is =N={_balance}");
        }

        // Define a method for displaying transaction history for thr account in a tabular form
        public void PrintHistory()
        {
            //Implement the method that will print history, HINT: use a FOR each loop
            Console.WriteLine("Date \t\t\t\t Transaction Type \t Narration \t\t Amount \t");
            //sort the transaction in descending order of transaction date so that most recent transaction will appear first.
            var sortedTransactions = _transactions.OrderByDescending(t => t.TransactionDate);
            foreach (var transaction in sortedTransactions)
            {
                Console.Write(transaction.TransactionDate.ToString("dd/MM/yyyy hh:mm tt"));
                Console.Write("\t");
                Console.Write(transaction.TransactionType.ToString());
                Console.Write("\t\t\t");
                Console.Write(transaction.Narration);
                Console.Write("\t\t");
                Console.Write(transaction.TransactionAmount.ToString("C2").PadLeft(20));
                Console.WriteLine();
            }
        }

        private void TrackTransaction(TransactionType type, double amount, string narration)
        {
            var newTransaction = new Transaction(DateTime.Now, type, amount, narration);
            _transactions.Add(newTransaction);
        }
    }
}