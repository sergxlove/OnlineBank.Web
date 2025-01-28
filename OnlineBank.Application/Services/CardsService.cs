using OnlineBank.DataAccess.Abstractions;

namespace OnlineBank.Application.Services
{
    public class CardsService
    {
        private readonly ICardsRepository _repository;

        public CardsService(ICardsRepository repository)
        {
            _repository = repository;
        }

        
    }
}
