﻿using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<User> SearchUser(string userName);

        Task<User> Login(string userName, string password);

        Task CreateUser(string userName, string password, string email);
    }
}