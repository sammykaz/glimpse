﻿using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;

namespace Glimpse.Core.Services.Data
{
    public class UserTempDataService: IUserTempDataService
    {
        private readonly IUserTempRepository _userRepository;
        private User2 _activeUser;
        public UserTempDataService(IUserTempRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User2> SearchUser(string userName)
        {
            return await _userRepository.SearchUser(userName);
        }

        public async Task<User2> Login(string userName, string password)
        {
            _activeUser = await _userRepository.Login(userName, password);
            return _activeUser;
        }

        public User2 GetActiveUser()
        {
            return _activeUser;
        }
    }
}