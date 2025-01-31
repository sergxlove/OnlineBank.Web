using OnlineBank.Application.Abstractions;
using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Contracts.Requests;
using OnlineBank.DataAccess.Contracts.Response;

namespace OnlineBank.Application.Services
{
    public class DataUsersService : IDataUsersService
    {
        private readonly IDataUsersRepository _repository;

        public DataUsersService(IDataUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddNewDataUserAsync(string firstname, string secondName, string lastName,
            string dateBirth, string passportData, string numberPhone, string email)
        {
            return await _repository.Add(Guid.NewGuid(), firstname, secondName, lastName, dateBirth,
                passportData, numberPhone, email);
        }

        public async Task<Guid> AddNewDataUserAsync(RequestDataUsers request)
        {
            return await _repository.Add(request);
        }

        public async Task<ResponseDataUserOnlyFullName?> GetOnlyFullNameAsync(Guid id)
        {
            return await _repository.GetOnlyFullName(id);
        }

        public async Task<DataUsers?> GetDataUserAsync(Guid id)
        {
            return await _repository.GetFull(id);
        }
    }
}
