using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IUserTempRepository
    {
        Task<User2> SearchUser(string userName);

        Task<User2> Login(string userName, string password);
    }
}