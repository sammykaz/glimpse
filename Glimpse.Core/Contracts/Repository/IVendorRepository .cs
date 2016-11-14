using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IVendorRepository
    {
        Task<Vendor> SearchVendor(string vendorName); //This will be changed to Task<Vendor> SearchVendor(string vendorName)

        Task<Vendor> GetVendorByUserNamePassword(string userName, string password);

        Task PostVendor(Vendor vendor);

        Task<List<Vendor>> GetVendors();
    }
}