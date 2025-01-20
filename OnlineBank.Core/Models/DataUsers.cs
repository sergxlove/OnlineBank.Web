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
        public string FirstName { get; } = string.Empty;

        public string SecondName { get; } = string.Empty;

        public string LastName { get; } = string.Empty;

        public string DateBirth { get; } = string.Empty;

        public string PassportData { get; } = string.Empty;

        public string NumberPhone { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public DateOnly DateRegistration { get; }

        private DataUsers(string firstName, string secondName, string lastName, string dateBirth, 
            string passportData, string numberPhone, string email)
        {
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

        public (DataUsers? dataUser, string error) Create(string firstName, string secondName, string lastName,
            string dateBirth, string passportData, string numberPhone, string email)
        {
            DataUsers? newDataUser = null;
            string error = string.Empty;

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

        public (DataUsers? dataUser, string error) Create(DataUsersRequest us)
        {
            return Create(us.FirstName, us.SecondName, us.LastName, us.DateBirth, us.PassportData, 
                us.NumberPhone, us.Email);
        }
    }
}
