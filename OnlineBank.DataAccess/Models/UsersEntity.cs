using OnlineBank.Core.Models;

namespace OnlineBank.DataAccess.Models
{
    public class UsersEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "user";

        public DataUsersEntity? DataUsers {  get; set; }

        public List<CardsEntity> Cards { get; set; } = [];

        public List<BankScoreEntity> BankScores { get; } = [];
    }
}
