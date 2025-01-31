using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Contracts.Requests;
using OnlineBank.DataAccess.Contracts.Response;

namespace OnlineBank.Application.Abstractions
{
    public interface IDataUsersService
    {
        Task<Guid> AddNewDataUserAsync(RequestDataUsers request);
        Task<Guid> AddNewDataUserAsync(string firstname, string secondName, string lastName, string dateBirth, string passportData, string numberPhone, string email);
        Task<DataUsers?> GetDataUserAsync(Guid id);
        Task<ResponseDataUserOnlyFullName?> GetOnlyFullNameAsync(Guid id);
    }
}
