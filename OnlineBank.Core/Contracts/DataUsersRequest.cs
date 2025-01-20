namespace OnlineBank.Core.Contracts
{
    public class DataUsersRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string DateBirth { get; set; } = string.Empty;

        public string PassportData { get; set; } = string.Empty;

        public string NumberPhone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
