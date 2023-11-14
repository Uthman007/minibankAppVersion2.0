using MiniBankApp2.Interfaces;
using MiniBankApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = MiniBankApp2.Models.Transaction; // Apply an Alias for our preffered transaction type to avoid conflict with a built in type of the same name

namespace MiniBankApp2.Implementations
{
    internal class ReportService : IReportService
    {
        // TAKE-HOME TASK: Implement this method by adding the necessary logic
        public void PrintAccountBeneficiaries(IList<Beneficiary> beneficiaries)
        {
            Console.WriteLine("Beneficiary Name \t\t\t Bank \t\t Account Number \t");
            foreach (var beneficiary in beneficiaries)
            {
                Console.Write(beneficiary.NickName);
                Console.Write("\t\t\t");
                Console.Write(beneficiary.BankName);
                Console.Write("\t\t");
                Console.Write(beneficiary.AccountNumber.ToString().PadLeft(15));
                Console.WriteLine();
            }
        }

        public void PrintAccountHistory(IList<Transaction> transactions)
        {
            //Implement the method that will print history, HINT: use a FOR each loop
            Console.WriteLine("Date \t\t\t\t Transaction Type \t Narration \t\t Amount \t");
            //sort the transaction in descending order of transaction date so that most recent transaction will appear first.
            var sortedTransactions = transactions.OrderByDescending(t => t.TransactionDate);
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
    }
}
