using OnlineBank.Core.Models;

namespace OnlineBank.DataAccess.Abstractions
{
    public interface IUsersRepository
    {
        Task<bool> Check(string login);
        Task<Guid> CreateAsync(Users users);
        Task<int> Delete(string login);
        Task<Users?> GetAsync(string login);
        Task<string?> GetPasswordAsync(string login);
        Task<int> UpdatePassword(string login, string password);
    }
}
