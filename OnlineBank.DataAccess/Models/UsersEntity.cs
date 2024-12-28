namespace OnlineBank.DataAccess.Models
{
    public class UsersEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string NumberCard { get; set; } = string.Empty;

        public string DateEnd { get; set; } = string.Empty;

        public string Cvv { get; set; } = string.Empty;
    }
}
