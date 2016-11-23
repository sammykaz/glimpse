
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Plugin.RestClient;
using Glimpse.Core.Services.General;
using System.Collections.Generic;

namespace Glimpse.Core.Services.Data
{
    public class UserDataService: IUserDataService
    {
        private readonly IUserRepository _userRepository;
        private User _activeUser;

        public UserDataService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<List<User>> SearchUser(string userName)
        {
            return await _userRepository.SearchUser(userName);
        }


        public User GetActiveUser()
        {
            return _activeUser;
        }

        public async Task SignUp(User user)
        {
            var cryptoTuple = Cryptography.EncryptAes(user.Password);
            user.Password = cryptoTuple.Item1;
            user.Salt = cryptoTuple.Item2;
            await _userRepository.PostUser(user);
        }

    }
}