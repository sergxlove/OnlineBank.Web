using OnlineBank.Application.Abstractions;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Contracts.Requests;
using System.Globalization;

namespace OnlineBank.Application.Services
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository _repository;
        private readonly ISystemTableRepository _systemRepository;

        public CardsService(ICardsRepository repository, ISystemTableRepository systemRepository)
        {
            _repository = repository;
            _systemRepository = systemRepository;
        }

        public async Task<Guid> AddNewCard(string numberCard, string dateEnd, string cvv, Guid userId)
        {
            return await _repository.Add(numberCard, dateEnd, cvv, userId);
        }

        public async Task<Guid> AddNewCard(RequestCards request)
        {
            return await _repository.Add(request);
        }

        public async Task<int> DeleteCard(string numberCard)
        {
            return await _repository.Delete(numberCard);
        }

        public async Task<Guid?> VerifyCard(string numberCard, string dateEnd, string cvv)
        {
            return await _repository.Verify(numberCard, dateEnd, cvv);
        }

        public async Task<string> GenerateNumberCard()
        {
            long number = await _systemRepository.GetAndIncrement();
            return "7700" + number.ToString();
        }

        public string GenerateDateEnd()
        {
            string date = DateOnly.FromDateTime(DateTime.UtcNow).ToString();
            string result = string.Concat(date[3], date[4], "/", date[8], date[9]);
            return result;
        }

        public string GenerateCvv()
        {
            return Convert.ToString(Random.Shared.Next(100, 999 + 1));
        }
    }
}
