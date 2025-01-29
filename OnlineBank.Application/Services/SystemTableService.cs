using OnlineBank.Application.Abstractions;
using OnlineBank.DataAccess.Abstractions;

namespace OnlineBank.Application.Services
{
    public class SystemTableService : ISystemTableService
    {
        private readonly ISystemTableRepository _repository;

        public SystemTableService(ISystemTableRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> GetNumberCard()
        {
            return await _repository.Get();
        }

        public async Task<int> IncrementNumberCard()
        {
            return await _repository.Increment();
        }

        public async Task<long> GetAndIncrementNumberCard()
        {
            return await _repository.GetAndIncrement();
        }

    }
}
