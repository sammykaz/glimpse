using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IVendorRepository
    {
        Task<User> SearchUser(string userName);

        Task<User> Login(string userName, string password);

        Task SignUp(string userName, string password, string email, string company);
    }
}