using System.Threading.Tasks;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> SearchUser(string userName);


        Task PostUser(User user);

        Task<List<User>> GetUsers();
    }
}