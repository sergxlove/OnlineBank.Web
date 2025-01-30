using OnlineBank.Core.Contracts;

namespace OnlineBank.Core.Models
{
    public class DataUsers
    {
        public const int MAX_LENGTH_FIRSTNAME = 30;
        public const int MAX_LENGTH_SECONDNAME = 30;
        public const int MAX_LENGTH_LASTNAME = 30;
        public const string FORMAT_DATABIRTH = "dd.mm.yyyy";
        public const string FORMAT_PASSPORTDATA = "ssss/nnnnnn";
        public const string FORMAT_NUMBERPHONE = "+7-XXX-XXX-XX-XX";
        public const int MIN_LENGTH_EMAIL = 5;
        public const int MAX_LENGTH_EMAIL = 40;

        public Guid Id { get; }
        public string FirstName { get; } = string.Empty;

        public string SecondName { get; } = string.Empty;

        public string LastName { get; } = string.Empty;

        public string DateBirth { get; } = string.Empty;

        public string PassportData { get; } = string.Empty;

        public string NumberPhone { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public DateOnly DateRegistration { get; }

        public Users? User { get; }

        private DataUsers(string firstName, string secondName, string lastName, string dateBirth, 
            string passportData, string numberPhone, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            DateBirth = dateBirth;
            PassportData = passportData;
            NumberPhone = numberPhone;
            Email = email;
            DateTime now = DateTime.Now;
            DateRegistration = new(now.Year, now.Month, now.Day);
        }

        private DataUsers(DataUsersRequest us) : 
            this(us.FirstName, us.SecondName, us.LastName, us.DateBirth,
                us.PassportData, us.NumberPhone, us.Email)
        {

        }

        public static (DataUsers? dataUser, string error) Create(string firstName, string secondName, string lastName,
            string dateBirth, string passportData, string numberPhone, string email)
        {
            DataUsers? newDataUser = null;
            string error = string.Empty;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(secondName) || string.IsNullOrEmpty(lastName))
            {
                error = $"Отсутствует ФИО";
                return (newDataUser, error);
            }

            if(firstName.Length > MAX_LENGTH_FIRSTNAME)
            {
                error = $"Имя дллжно иметь до {MAX_LENGTH_FIRSTNAME} символов";
                return (newDataUser, error);
            }

            if (secondName.Length > MAX_LENGTH_SECONDNAME)
            {
                error = $"Отчество дллжно иметь до {MAX_LENGTH_SECONDNAME} символов";
                return (newDataUser, error);
            }

            if (lastName.Length > MAX_LENGTH_LASTNAME)
            {
                error = $"Фамилия дллжно иметь до {MAX_LENGTH_LASTNAME} символов";
                return (newDataUser, error);
            }

            if (string.IsNullOrEmpty(dateBirth))
            {
                error = $"Отсутствует дата рождения";
                return (newDataUser, error);
            }

            if (dateBirth[2] != FORMAT_DATABIRTH[2] || dateBirth[5] != FORMAT_DATABIRTH[5] 
                || dateBirth.Any(char.IsLetter) || dateBirth.Length != FORMAT_DATABIRTH.Length)
            {
                error = $"Некорректный формат даты рождения";
                return (newDataUser, error);
            }

            if (string.IsNullOrEmpty(passportData))
            {
                error = $"Отсутствуют паспортные данные"; 
                return (newDataUser, error);
            }

            if (passportData[4] != FORMAT_PASSPORTDATA[4] 
                || passportData.Any(char.IsLetter) 
                || passportData.Length != FORMAT_PASSPORTDATA.Length)
            {
                error = $"Некорректный формат паспортных данных";
                return (newDataUser, error);
            }

            if(string.IsNullOrEmpty(numberPhone))
            {
                error = $"Отсутствует номер телефона";
                return (newDataUser, error);
            }
            
            if(numberPhone.Length != FORMAT_NUMBERPHONE.Length || numberPhone.Any(char.IsLetter))
            {
                error = $"Некорректный формат номера телефона";
                return (newDataUser, error);
            }

            int[] positionSympols = { 0, 2, 6, 10, 13 };
            foreach (int i in positionSympols) 
            {
                if (numberPhone[i] != FORMAT_NUMBERPHONE[i])
                {
                    error = $"Некорректный формат номера телефона";
                    return (newDataUser, error);
                }
            }

            if(string.IsNullOrEmpty(email))
            {
                error = $"Отсутствует электронная почта";
                return (newDataUser, error);
            }

            if(email.Length < MIN_LENGTH_EMAIL || email.Length > MAX_LENGTH_EMAIL)
            {
                error = $"Электронная почта должна иметь от {MIN_LENGTH_EMAIL} " +
                    $"до {MAX_LENGTH_EMAIL} символов";
                return (newDataUser, error);
            }

            newDataUser = new(new() 
            {
                FirstName = firstName,
                SecondName = secondName,
                LastName = lastName,
                DateBirth = dateBirth,
                PassportData = passportData,
                NumberPhone = numberPhone,
                Email = email
            });
            return (newDataUser, error);
        }

        public static (DataUsers? dataUser, string error) Create(DataUsersRequest us)
        {
            return Create(us.FirstName, us.SecondName, us.LastName, us.DateBirth, us.PassportData, 
                us.NumberPhone, us.Email);
        }
    }
}
