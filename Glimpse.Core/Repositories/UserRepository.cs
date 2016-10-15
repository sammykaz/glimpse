using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;

namespace Glimpse.Core.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        private static readonly List<User> AllKnownUsers = new List<User>
        {
            new User { UserName = "gillcleeren", Password="123456", UserId = 1}, //extremely secure, don't try this at home
            new User { UserName = "johnsmith", Password="789456", UserId = 2},
            new User { UserName = "annawhite", Password="100000", UserId = 3}
        };

        public async Task<User> SearchUser(string userName)
        {
            return await Task.FromResult(AllKnownUsers.FirstOrDefault(u => u.UserName == userName));
        }

        public async Task<User> Login(string userName, string password)
        {
            return await Task.FromResult(AllKnownUsers.FirstOrDefault(u => u.UserName == userName && u.Password == password));
        }
    }
}
