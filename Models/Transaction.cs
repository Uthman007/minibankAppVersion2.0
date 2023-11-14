using MiniBankApp2.Enums;

namespace MiniBankApp2.Models
{
    public record Transaction(DateTime TransactionDate, TransactionType TransactionType, double TransactionAmount)
    {
        public string Narration { get; set; }
    }
}
