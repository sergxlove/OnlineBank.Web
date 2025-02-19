namespace OnlineBank.DataAccess.Models
{
    public class BankScoreEntity
    {
        public Guid Id { get; set; }

        public string Number { get; set; } = string.Empty;

        public string CurrencyCode { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string AccountType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Guid UsersId { get; set; }

        public UsersEntity? Users { get; set; }

        public Guid? CardId { get; set; }

        public CardsEntity? Cards { get; set; }
    }
}
