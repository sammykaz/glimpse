﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;

namespace Glimpse.Core.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public async Task<User> SearchUser(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task SignUp(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }
    }
}
