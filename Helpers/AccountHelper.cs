using MiniBankApp2.Implementations;
using MiniBankApp2.Models;

namespace MiniBankApp2.Helpers
{
    internal static class AccountHelper
    {
        public static int GenerateAccountNumber()
        {
            int accountNumber;
            var randomNumberGenerator = new Random();
            accountNumber = randomNumberGenerator.Next(1000000000, int.MaxValue);
            return accountNumber;
        }

        public static Account MapBankAccountToAccount(BankAccount bankAccount)
        {
            if (bankAccount == null)
                return new Account();

            return new Account()
            {
                FirstName = bankAccount.AccountName.Split(' ')[0], // Extract and assign first name
                LastName = bankAccount.AccountName.Split(' ')[1], // Extract and assign last name
                AccountNumber = bankAccount.AccountNumber,
                CurrentBalance = bankAccount.AccountBalance,
                BankName = bankAccount.Bank,
                AccountType = bankAccount.AccountType,
                Transactions = bankAccount.Transactions,
                Beneficiaries = bankAccount.Beneficiaries
            };
        }
    }

}
