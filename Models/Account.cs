using MiniBankApp2.Enums;

namespace MiniBankApp2.Models
{
    public record Account()
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AccountNumber { get; set; }

        public double CurrentBalance { get; set; }

        public string BankName { get; set; }

        public AccountType AccountType { get; set; }

        public List<Transaction> Transactions { get; set; }

        public List<Beneficiary> Beneficiaries { get; set; }
    }
}

