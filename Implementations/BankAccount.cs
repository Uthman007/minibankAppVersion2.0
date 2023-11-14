using MiniBankApp2.Enums;
using MiniBankApp2.Helpers;
using MiniBankApp2.Interfaces;
using MiniBankApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        private readonly IReportService _reportService;        
        private List<Transaction> _transactions = new List<Transaction>();
        private List<Beneficiary> _beneficiaries = new List<Beneficiary>();

        private const string _bankName = "THE BULB MINI BANK";

        // These are properties that make pieces of data/state available to external classes
        public string AccountName => $"{_firstName} {_lastName}".ToUpper();
        public int AccountNumber => _accountNumber;
        public AccountType AccountType => _accountType;
        public string Bank => _bankName;
        public double AccountBalance => _balance;

        // Define a method for opening account. We can use the constructor for this purpose
        public BankAccount(string firstName, string lastName, double initialBalance, AccountType accountType, IReportService reportService)
        {
            _reportService = reportService;
            _firstName = firstName;
            _lastName = lastName;
            _balance = initialBalance;
            _accountType = accountType;
            _accountNumber = AccountHelper.GenerateAccountNumber();
            TrackTransaction(TransactionType.Deposit, initialBalance, "Initial Deposit");
        }

        public BankAccount(Account existingAccount, IReportService reportService)
        {
            _reportService = reportService;
            _firstName = existingAccount.FirstName;
            _lastName = existingAccount.LastName;
            _balance = existingAccount.CurrentBalance;
            _accountType = existingAccount.AccountType;
            _accountNumber = existingAccount.AccountNumber;
            _transactions = existingAccount.Transactions;
            _beneficiaries = existingAccount.Beneficiaries;
        }

        public BankAccount()
        {

        }

        public List<Transaction> Transactions => _transactions;

        public List<Beneficiary> Beneficiaries => _beneficiaries;

        // Define a method for depositing funds
        public void Deposit(double amount, string? narration)
        {
            //validate the amount
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount! Kindly ensure that 'Amount' is greater than zero.");
                return;
            }

            // Add the deposited amount for withdrawing funds
            _balance += amount;
            if (string.IsNullOrWhiteSpace(narration))
            {
                narration = "Cash Deposit";
            }
            TrackTransaction(TransactionType.Deposit, amount, narration);

            //Display the new balance
            DisplayBalance();
            Console.WriteLine(amount + " Deposited successfully!");
        }

        // Define a method for withdrawing funds
        public void Withdraw(double amount, string? narration)
        {
            //Validate the amount
            if (amount > _balance)
            {
                Console.WriteLine("Insufficient funds! Kindly enter an amount not greater than your current balance.");
                return;
            }

            //Deduct the withdrawn amount from the balance
            _balance -= amount;
            if (string.IsNullOrWhiteSpace(narration))
            {
                narration = "Cash Withdrawal";
            }
            TrackTransaction(TransactionType.Withdrawal, amount, narration);

            //Display the new balance
            DisplayBalance();
            Console.WriteLine(amount + "Has been Withdrawn successfully!");
           
        }

        // Define a method for displaying funds
        public void DisplayBalance()
        {
            Console.WriteLine($"Your current balance is =N={_balance}");
        }

        // Define a method for displaying transaction history for thr account in a tabular form
        public void PrintHistory()
        {
            _reportService.PrintAccountHistory(_transactions);
        }

        private void TrackTransaction(TransactionType type, double amount, string narration)
        {
            var newTransaction = new Transaction(DateTime.Now, type, amount);
            newTransaction.Narration = narration;
            _transactions.Add(newTransaction);
        }

        public void AddBeneficiary()
        {
            try
            {
                Console.WriteLine("Enter the name of your beneficiary's bank: ");
                string beneficiaryBank = Console.ReadLine();

                Console.WriteLine("Enter your beneficiary's account number: ");
                int beneficiaryAccountNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter a name or nickname for your beneficiary: ");
                var beneficiaryNickname = Console.ReadLine();

                // Check if this account number has been added previously
                if (_beneficiaries.Exists(b => b.AccountNumber == beneficiaryAccountNumber))
                {
                    Console.WriteLine("This beneficiary account number already exists.");
                    return;
                }

                var newBeneficiary = new Beneficiary(beneficiaryBank, beneficiaryAccountNumber, beneficiaryNickname);
                _beneficiaries.Add(newBeneficiary);
                Console.WriteLine("A beneficiary with the following details: " + " " + beneficiaryNickname + ", " + beneficiaryAccountNumber + ", " + beneficiaryBank +"."+ " has been added to the list of beneficiary successfully." );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to add the beneficiary. {ex.Message}");                
            }
        }

        public void ViewBeneficiaries()
        {
            _reportService.PrintAccountBeneficiaries(_beneficiaries);
        }
    }
}