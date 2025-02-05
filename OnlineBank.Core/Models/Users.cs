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
        public Guid Id { get; }

        public string Login { get; } = string.Empty;

        public string Password { get; } = string.Empty;

        public string Role { get; } = string.Empty;

        public DataUsers? DataUsers { get; }

        public List<Cards> Cards { get; } = [];

        public static (Users? user, string error) Create(string login, string password)
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
            if (password.Length < MIN_LENGTH_PASSWORD || password.Length > MAX_LENGTH_PASSWORD)
            {
                error = $"Значение password должно иметь от {MIN_LENGTH_PASSWORD} до {MAX_LENGTH_PASSWORD} символов";
                return (newUser, error);
            }
            
            newUser = new(Guid.NewGuid(), login, password);
            return (newUser, error);
        }


    }
}
