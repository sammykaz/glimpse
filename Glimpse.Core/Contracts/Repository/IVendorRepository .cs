using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> SearchVendor(string vendorName);

        Task PostVendor(Vendor vendor);

        Task<List<Vendor>> GetVendors();

        Task<int> GetVendorId(string username);
    }
}