using Microsoft.EntityFrameworkCore;
using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly DbContextSqlite _context;

        public UsersRepository(DbContextSqlite context)
        {
            _context = context;
        }

        public async Task<bool> Check(string login)
        {
            var usserEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Login == login);
            if (usserEntity is null)
            {
                return false;
            }
            return true;
        }

        public async Task<Guid> CreateAsync(Users users)
        {
            var usersEntity = new UsersEntity()
            {
                Id = users.Id,
                Login = users.Login,
                Password = users.Password,
                NumberCard = users.NumberCard,
                DateEnd = users.DateEnd,
                Cvv = users.Cvv,
            };
            await _context.Users.AddAsync(usersEntity);
            await _context.SaveChangesAsync();
            return usersEntity.Id;
        }

        public async Task<Users?> GetAsync(string login)
        {
            var userEntity = await _context.Users.
                AsNoTracking()
                .FirstOrDefaultAsync(a => a.Login == login);
            if(userEntity is null)
            {
                return null;
            }
            return Users.Create(userEntity.Login, userEntity.Password, userEntity.NumberCard, userEntity.DateEnd, userEntity.Cvv).user;
        }

        public async Task<string?> GetPasswordAsync(string login)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Login == login);
            if(userEntity is null)
            {
                return null;
            }
            return userEntity.Password;
        }

        public async Task<int> UpdatePassword(string login, string password)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.Login == login)
                .ExecuteUpdateAsync(s => 
                s.SetProperty(a => a.Password, password));
        }

        public async Task<int> Update(Users user)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.Login == user.Login)
                .ExecuteUpdateAsync(s => s.SetProperty(a => 
                a.NumberCard, user.NumberCard)
                .SetProperty(a => a.DateEnd, user.DateEnd)
                .SetProperty(a => a.Cvv, user.Cvv));
        }

        public async Task<int> Delete(string login)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.Login == login)
                .ExecuteDeleteAsync();
        }
    }
}
