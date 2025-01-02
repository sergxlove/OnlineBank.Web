namespace OnlineBank.Infrastructure.Abstractions
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool Validate(string solve);
        bool Verify(string password, string passwordHash);
    }
}
