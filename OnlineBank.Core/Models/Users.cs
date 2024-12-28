namespace OnlineBank.Core.Models
{
    public class Users
    {
        public const int MIN_LENGTH_LOGIN = 5;
        public const int MAX_LENGTH_LOGIN = 20;
        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH_PASSWORD = 50;
        public const int SIZE_NUMBERCARD = 16;
        public const string DATEEND_FORMAT = "mm/gg";
        public const int SIZE_CVV = 3;
        private Users(Guid id, string login, string password, string numberCard, string dateEnd, string cvv)
        {
            Id = id;
            Login = login;
            Password = password;
            NumberCard = numberCard;
            DateEnd = dateEnd;
            Cvv = cvv;
        }
        public Guid Id { get; }

        public string Login { get; } = string.Empty;

        public string Password { get; } = string.Empty;

        public string NumberCard { get; } = string.Empty;

        public string DateEnd { get; } = string.Empty;

        public string Cvv { get; } = string.Empty;

        public static (Users? user, string error) Create(Guid id, string login, string password, string numberCard, string dateEnd, string cvv)
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
            if (string.IsNullOrEmpty(numberCard))
            {
                error = $"Отстутствует значение number card";
                return (newUser, error);
            }
            if(numberCard.Length != SIZE_NUMBERCARD)
            {
                error = $"Размер значения number card должен быть равен {SIZE_NUMBERCARD} символов";
                return (newUser, error);
            }
            if(string.IsNullOrEmpty(dateEnd))
            {
                error = $"Отсутствует значение dateEnd";
                return (newUser, error);
            }
            if(dateEnd.Length != DATEEND_FORMAT.Length)
            {
                error = $"Значение dateEnd должно иметь формат : {DATEEND_FORMAT}";
                return (newUser, error);
            }
            if(string.IsNullOrEmpty(cvv))
            {
                error = $"Отсутствует значение cvv";
                return (newUser, error);
            }
            if(cvv.Length != SIZE_CVV)
            {
                error = $"Размер значения cvv должен быть равным {SIZE_CVV} символов";
                return (newUser, error);
            }
            newUser = new(Guid.NewGuid(), login, password, numberCard, dateEnd, cvv);
            return (newUser, error);
        }
    }
}
