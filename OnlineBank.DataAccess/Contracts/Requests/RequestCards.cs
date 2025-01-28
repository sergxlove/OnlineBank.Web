namespace OnlineBank.DataAccess.Contracts.Requests
{
    public class RequestCards
    {
        public string NumberCard { get; set; } = string.Empty;
        public string DateEnd { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
