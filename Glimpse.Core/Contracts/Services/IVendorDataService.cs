using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface IVendorDataService
    {
        Task<List<Vendor>> SearchUser(string userName);


        Task SignUp(Vendor vendor);

        Vendor GetActiveVendor();
    }
}