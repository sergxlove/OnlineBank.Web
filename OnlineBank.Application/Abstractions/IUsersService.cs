﻿using OnlineBank.Core.Models;

namespace OnlineBank.Application.Abstractions
{
    public interface IUsersService
    {
        Task<bool> CheckHaveUserAsync(string login);
        Task<Guid> CreateNewUserAsync(Users user);
        Task<int> DeleteUserAsync(string login);
        Task<string?> GetPasswordUserAsync(string login);
        Task<Users?> GetUserAsync(string login);
        Task<int> UpdateDataUserAsync(Users user);
        Task<int> UpdatePasswordUserAsync(string login, string password);
    }
}