using OnlineBank.Application.Abstractions;
using OnlineBank.Infrastructure.Abstractions;

namespace OnlineBank.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly IPasswordHasher _passwordHasher;

        public PasswordHasherService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string GenerateHashPassword(string password)
        {
            return _passwordHasher.Generate(password);
        }

        public bool ValidateSolve(string solve)
        {
            return _passwordHasher.Validate(solve);
        }

        public bool VerifyUser(string password, string hashPassword)
        {
            return _passwordHasher.Verify(password, hashPassword);
        }
    }
}
