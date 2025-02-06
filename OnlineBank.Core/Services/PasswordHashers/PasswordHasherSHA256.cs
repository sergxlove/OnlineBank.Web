using OnlineBank.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBank.Core.Services.PasswordHashers
{
    public class PasswordHasherSHA256 : IPasswordHasherSHA256
    {
        public string Generate(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool Verify(string password, string passwordHash)
        {
            if (Generate(password) == passwordHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
