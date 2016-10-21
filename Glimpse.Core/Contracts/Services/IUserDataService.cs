using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IUserDataService
    {
        Task<User2> SearchUser(string userName);

        Task<User2> Login(string userName, string password);

        User2 GetActiveUser();
    }
}