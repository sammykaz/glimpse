using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;

namespace Glimpse.Core.Services.Data
{
    public class UserDataService: IUserDataService
    {
        private readonly IUserRepository _userRepository;
        private User _activeUser;

        public UserDataService() { }

        public UserDataService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> SearchUser(string userName)
        {
            return await _userRepository.SearchUser(userName);
        }

        public async Task<User> Login(string userName, string password)
        {
            _activeUser = await _userRepository.Login(userName, password);
            return _activeUser;
        }

        public User GetActiveUser()
        {
            return _activeUser;
        }

        public async Task SignUp(string userName, string password, string email)
        {
            await _userRepository.SignUp(userName, password, email);
        }

        public List<User> GetUsers()
        {
            var users = new List<User>
            {
               new User
            {
                Email = "asdf1@gmail.com",
                FirstName = "asdf1",
                Password = "yolo"
            },
               new User
            {
                Email = "asdf2@gmail.com",
                FirstName = "asdf2",
                Password = "yolo"
            },
                  new User
            {
                Email = "asdf3@gmail.com",
                FirstName = "asdf3",
                Password = "yolo"
            }
        };


            return users;
        }
    }
}