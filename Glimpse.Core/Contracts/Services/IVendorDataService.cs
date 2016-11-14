using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IVendorDataService
    {
        Task<Vendor> SearchUser(string userName);

        Task<Vendor> Login(string userName, string password);

        Task SignUp(Vendor vendor);

        Vendor GetActiveVendor();
    }
}