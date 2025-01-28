using Microsoft.EntityFrameworkCore;
using OnlineBank.DataAccess.Abstractions;

namespace OnlineBank.DataAccess.Repositories
{
    public class SystemTableRepository : ISystemTableRepository
    {
        private readonly DbContextSqlite _context;

        public SystemTableRepository(DbContextSqlite context)
        {
            _context = context;
        }

        public async Task<long> Get()
        {
            int id = 777;
            var table = await _context.SystemTable
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return table!.NumberCard;
        }

        public async Task<int> Increment()
        {
            int id = 777;
            long numberCard = await Get() + 1;
            return await _context.SystemTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.NumberCard, numberCard));
        }

        public async Task<long> GetAndIncrement()
        {
            long numberCard = await Get();
            await Increment();
            return numberCard;
        }
    }
}

