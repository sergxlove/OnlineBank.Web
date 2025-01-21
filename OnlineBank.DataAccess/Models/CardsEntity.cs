namespace OnlineBank.DataAccess.Models
{
    public class CardsEntity
    {
        public Guid Id { get; set; }

        public string NumberCard { get; set; } = string.Empty;

        public string DateEnd { get; set; } = string.Empty;

        public string Cvv { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public UsersEntity? User { get; set; }
    }
}
