using Microsoft.EntityFrameworkCore;
using OnlineBank.Core.Contracts;
using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Contracts.Requests;
using OnlineBank.DataAccess.Contracts.Response;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Repositories
{
    public class DataUsersRepository : IDataUsersRepository
    {
        private readonly DbContextSqlite _context;

        public DataUsersRepository(DbContextSqlite context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Guid id, string firstname, string secondName, string lastName, string dateBirth,
            string passportData, string numberPhone, string email)
        {
            DataUsersEntity newData = new()
            {
                Id = id,
                FirstName = firstname,
                SecondName = secondName,
                LastName = lastName,
                DateBirth = dateBirth,
                PassportData = passportData,
                NumberPhone = numberPhone,
                Email = email,
                DateRegistration = DateOnly.FromDateTime(DateTime.UtcNow)
            };
            await _context.AddAsync(newData);
            await _context.SaveChangesAsync();

            return newData.Id;
        }

        public async Task<Guid> Add(RequestDataUsers r)
        {
            return await Add(r.Id, r.FirstName, r.SecondName, r.LastName, r.DateBirth, r.PassportData,
                r.NumberPhone, r.Email);
        }

        public async Task<ResponseDataUserOnlyFullName?> GetOnlyFullName(Guid id)
        {
            var user = await _context.DataUsers.FirstOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return null;
            }
            return new ResponseDataUserOnlyFullName()
            {
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                LastName = user.LastName
            };
        }

        public async Task<DataUsers?> GetFull(Guid id)
        {
            var user = await _context.DataUsers.FirstOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return null;
            }
            DataUsersRequest userRequest = new()
            {
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                LastName = user.LastName,
                DateBirth = user.DateBirth,
                PassportData = user.PassportData,
                NumberPhone = user.NumberPhone,
                Email = user.Email
            };
            return DataUsers.Create(userRequest).dataUser;
        }

    }
}
