using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IUserDataService
    {
        Task<List<User>> SearchUser(string userName);

        Task SignUp(User user);

        User GetActiveUser();
    }
}