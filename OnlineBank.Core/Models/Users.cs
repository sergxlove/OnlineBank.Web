using System.Security.Cryptography;
using System.Text;

namespace OnlineBank.Core.Models
{
    public class Users
    {
        public const int MIN_LENGTH_LOGIN = 5;
        public const int MAX_LENGTH_LOGIN = 20;
        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH_PASSWORD = 50;

        private Users(Guid id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = "user";
        }

        public Users(Users user)
        {
            Id = user.Id;
            Login = user.Login;
            Password = user.Password;
            Role = user.Role;
        }

        public Guid Id { get; }

        public string Login { get; } = string.Empty;

        public string Password { get; } = string.Empty;

        public string Role { get; } = string.Empty;

        public DataUsers? DataUsers { get; }

        public List<Cards> Cards { get; } = [];

        public List<BankScore> BankScores { get; } = [];

        public static (Users? user, string error) Create(string login, string password, Func<string, string> hashMethod)
        {
            Users? newUser = null;
            string error = string.Empty;
            if (string.IsNullOrEmpty(login))
            {
                error = $"Отсутствует значение login";
                return (newUser, error);
            }
            if (login.Length < MIN_LENGTH_LOGIN || login.Length > MAX_LENGTH_LOGIN)
            {
                error = $"Значение login должно иметь от {MIN_LENGTH_LOGIN} до {MAX_LENGTH_LOGIN} символов";
                return (newUser, error);
            }
            if (string.IsNullOrEmpty(password))
            {
                error = $"Отсутствует значение password";
                return (newUser, error);
            }
            if (password.Length < MIN_LENGTH_PASSWORD)
            {
                error = $"Значение password должно иметь от {MIN_LENGTH_PASSWORD} до {MAX_LENGTH_PASSWORD} символов";
                return (newUser, error);
            }
            
            newUser = new(Guid.NewGuid(), login, hashMethod(password));
            return (newUser, error);
        }

        public static (Users? user, string error) Create(string login, string password)
        {
            return Create(login, password, a =>
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(a));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            });
        }
    }
}
