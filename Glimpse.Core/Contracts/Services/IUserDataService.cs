using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IUserDataService
    {
        Task<User> SearchUser(string userName);

        Task<User> Login(string userName, string password);

        User GetActiveUser();

        Task SignUp(string userName, string password, string email);

    }
}