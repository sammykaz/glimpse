using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IVendorRepository : IUserRepository
    {
        Task CreateUser(string userName, string password, string email, string company);
    }
}