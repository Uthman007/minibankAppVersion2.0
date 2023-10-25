using MiniBankApp2.Enums;
using MiniBankApp2.Implementations;
using MiniBankApp2.Models;

public class program
{
    public static void Main(string[] args)
    {
        string firstName;
        string lastName;
        double initialBalance;
        AccountType accountType;

        Console.WriteLine("----------YOU ARE WELCOME TO ALAO BANK-----------");
        Console.WriteLine("Please create an account for yourself......!");
        Console.Write("Input your first name: ");
        firstName = Console.ReadLine();
        Console.Write("Input your last name: ");
        lastName = Console.ReadLine();
        Console.Write("Kindly indicate the account type you would like to have using 1. savings 2. current 3. Domiciliary 4. fixed: ");
        //casting from string to integer and to enum type TODO: ensure input is between 1 to 4.
        accountType = (AccountType) int.Parse( Console.ReadLine());
        Console.Write("Kindly type in the amount you would like to start with: ");
        var balanceString = Console.ReadLine();
        double.TryParse(balanceString, out double doubleBalance);
        initialBalance = doubleBalance;

        // Instantiate the BankAccount class to open an account
        var account = new BankAccount(firstName, lastName, initialBalance, accountType);

        // Display current balance
        account.DisplayBalance();

        Console.WriteLine();
        Console.WriteLine();

        int userDecision;
        do
        {
            int userChoice;
            do
            {
                Console.WriteLine("Welcome! What would you like to do?\n1. Deposit Funds 2. Withdraw cash 3. View Statement");
                bool userChoiceValid = int.TryParse(Console.ReadLine(), out userChoice);
                if (!userChoiceValid)
                {
                    Console.WriteLine("Invalid input! Please enter a valid number");
                    Console.WriteLine();
                }
            } while (userChoice != 1 && userChoice != 2 && userChoice != 3);
            if (userChoice == 1)
            {  //make deposit
                Console.WriteLine("kindly enter an amount to deposit");
                //TODO: validation is required
                var amountToDeposit = Convert.ToDouble(Console.ReadLine());
                account.Deposit(amountToDeposit);
                Console.WriteLine();
            }
            else if (userChoice == 2)
            {
                //make a withdrawal
                Console.WriteLine("kindly enter an amount to withdraw");
                //TODO: validation is required
                var amountToWithdraw = Convert.ToDouble(Console.ReadLine());
                account.Withdraw(amountToWithdraw);
                Console.WriteLine();
            }
            else if (userChoice == 3)
            {
                account.PrintHistory();
            }
            else
            {
                Console.WriteLine("Wrong Input detected. Please enter a valid input");
                Console.WriteLine();
            }

            Console.WriteLine("Would you like to perform another transaction? \nEnter 1 for \"Yes\" 2 for \"No\"");
            Console.WriteLine();
            bool userDecisionSuccess = int.TryParse(Console.ReadLine(), out userDecision);
        } while (userDecision == 1);

        if (userDecision == 2)
        {
            Console.WriteLine("Bye! Thanks for banking with us :)");
            Console.WriteLine();
        }

    }
}
