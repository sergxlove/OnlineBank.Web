namespace OnlineBank.Core.Contracts
{
    public class CardsRequest
    {
        public string NumberCard { get; set; } = string.Empty;

        public string DateEnd { get; set; } = string.Empty;

        public string Cvv { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
