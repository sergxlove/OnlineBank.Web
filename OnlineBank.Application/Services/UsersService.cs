﻿using OnlineBank.Application.Abstractions;
using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Abstractions;

namespace OnlineBank.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> CheckHaveUserAsync(string login)
        {
            return await _usersRepository.Check(login);
        }

        public async Task<Guid> CreateNewUserAsync(Users user)
        {
            return await _usersRepository.CreateAsync(user);
        }

        public async Task<Users?> GetUserAsync(string login)
        {
            return await _usersRepository.GetAsync(login);
        }

        public async Task<string?> GetPasswordUserAsync(string login)
        {
            return await _usersRepository.GetPasswordAsync(login);
        }

        public async Task<string> GetLoginUserAsync(Guid? id)
        {
            return await _usersRepository.GetLoginAsync(id);
        }

        public async Task<string> GetRoleUserAsync(string login)
        {
            return await _usersRepository.GetRoleAsync(login);
        }

        public async Task<int> UpdatePasswordUserAsync(string login, string password)
        {
            return await _usersRepository.UpdatePassword(login, password);
        }

        public async Task<int> UpdateLoginAndPasswordAsync(Guid id, string login, string password)
        {
            return await _usersRepository.UpdateLoginAndPassword(id, login, password);
        }

        public async Task<int> DeleteUserAsync(string login)
        {
            return await _usersRepository.Delete(login);
        }
    }
}
