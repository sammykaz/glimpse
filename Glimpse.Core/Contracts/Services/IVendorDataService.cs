using System.Threading.Tasks;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace Glimpse.Core.Contracts.Services
{
    public interface IVendorDataService
    {
        Task<User> SearchUser(string userName);

        Task<User> Login(string userName, string password);

        Task SignUp(Vendor vendor);

        User GetActiveUser();

        Task<List<Vendor>> GetVendors();
    }
}