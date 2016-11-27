using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace Glimpse.Core.Contracts.Services
{
    public interface IVendorDataService
    {
        Task<List<Vendor>> SearchUser(string userName);

        Task SignUp(Vendor vendor);

        Vendor GetActiveVendor();

        Task<int> GetVendorId(string username);

        Task<List<Vendor>> GetVendors();

        Task AddVendorPromotion(Vendor vendor);
    }
}