namespace OnlineBank.DataAccess.Models
{
    public class UsersEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DataUsersEntity? DataUsers {  get; set; }

        public List<CardsEntity> Cards { get; set; } = [];
    }
}
