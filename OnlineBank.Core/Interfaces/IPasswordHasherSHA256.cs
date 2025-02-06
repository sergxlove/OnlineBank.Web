namespace OnlineBank.Core.Interfaces
{
    public interface IPasswordHasherSHA256
    {
        string Generate(string password);
        bool Verify(string password, string passwordHash);
    }
}
