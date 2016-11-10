﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;
using Plugin.RestClient;

namespace Glimpse.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> SearchUser(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task PostUser(User user)
        {
            RestClient<User> restClient = new RestClient<User>();

            await restClient.PostAsync(user);      
        }

        public async Task<List<User>> GetUsers()
        {
            RestClient<User> restClient = new RestClient<User>();

            var usersList = await restClient.GetAsync();

            return usersList;
        }
    }
}
