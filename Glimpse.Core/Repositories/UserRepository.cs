using System.Collections.Generic;
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
        public async Task<List<User>> SearchUser(string userName)
        {
           RestClient<User> restClient = new RestClient<User>();
           return await restClient.GetUsersAsync(userName);
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
