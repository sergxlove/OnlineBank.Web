using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Contracts.Requests;
using OnlineBank.DataAccess.Contracts.Response;

namespace OnlineBank.DataAccess.Abstractions
{
    public interface IDataUsersRepository
    {
        Task<Guid> Add(Guid id, string firstname, string secondName, string lastName, string dateBirth, string passportData, string numberPhone, string email);
        Task<Guid> Add(RequestDataUsers r);
        Task<DataUsers?> GetFull(Guid id);
        Task<ResponseDataUserOnlyFullName?> GetOnlyFullName(Guid id);
    }
}
