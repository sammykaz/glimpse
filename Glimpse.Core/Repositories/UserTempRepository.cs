using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;

namespace Glimpse.Core.Repositories
{
    public class UserTempRepository : BaseRepository, IUserTempRepository
    {

        private static readonly List<User2> AllKnownUsers = new List<User2>
        {
            new User2 { UserName = "gillcleeren", Password="123456", UserId = 1}, //extremely secure, don't try this at home
            new User2 { UserName = "johnsmith", Password="789456", UserId = 2},
            new User2 { UserName = "annawhite", Password="100000", UserId = 3}
        };

        public async Task<User2> SearchUser(string userName)
        {
            return await Task.FromResult(AllKnownUsers.FirstOrDefault(u => u.UserName == userName));
        }

        public async Task<User2> Login(string userName, string password)
        {
            return await Task.FromResult(AllKnownUsers.FirstOrDefault(u => u.UserName == userName && u.Password == password));
        }
    }
}
