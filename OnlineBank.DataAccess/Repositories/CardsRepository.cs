using Microsoft.EntityFrameworkCore;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Repositories
{
 
    public class CardsRepository : ICardsRepository
    {
        private readonly DbContextSqlite _context;

        public CardsRepository(DbContextSqlite context)
        {
            _context = context;
        }

        public async Task<Guid> Add(string numberCard, string dateEnd, string cvv, Guid userId)
        {
            var newCard = new CardsEntity()
            {
                Id = Guid.NewGuid(),
                NumberCard = numberCard,
                DateEnd = dateEnd,
                Cvv = cvv,
                UserId = userId
            };
            var cards = await _context.AddAsync(newCard);
            await _context.SaveChangesAsync();
            return newCard.Id;
        }

        public async Task<int> Delete(string numberCard)
        {
            return await _context.Cards
                .AsNoTracking()
                .Where(a => a.NumberCard == numberCard)
                .ExecuteDeleteAsync();
        }
    }
}
