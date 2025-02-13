namespace OnlineBank.Core.Models
{
    public class BankScore
    {
        public Guid Id { get; }

        public string Number { get; } = string.Empty;

        public string CurrencyCode { get; } = string.Empty;

        public decimal Balance { get; }

        public string AccountType { get; } = string.Empty;

        public string Status { get; } = string.Empty;

        public Guid UsersId { get; } 



    }
}
