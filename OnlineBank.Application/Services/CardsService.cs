using OnlineBank.Application.Abstractions;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Contracts.Requests;

namespace OnlineBank.Application.Services
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository _repository;

        public CardsService(ICardsRepository repository)
        {
            _repository = repository;
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
    }
}
