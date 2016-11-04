using System.Threading.Tasks;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<User> SearchUser(string userName);

        Task<User> Login(string userName, string password);

        Task PostUserAsync(User user);

        Task<List<User>> GetUsersAsync();
    }
}