namespace OnlineBank.Application.Abstractions
{
    public interface IPasswordHasherService
    {
        string GenerateHashPassword(string password);
        bool ValidateSolve(string solve);
        bool VerifyUser(string password, string hashPassword);
    }
}
