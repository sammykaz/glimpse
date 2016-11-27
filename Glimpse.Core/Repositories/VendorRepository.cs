using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;
using Plugin.RestClient;

namespace Glimpse.Core.Repositories
{
    public class VendorRepository : IVendorRepository
    {
 
        public async Task<List<Vendor>> SearchVendor(string vendorName)
        {
            RestClient<Vendor> restClient = new RestClient<Vendor>();
            return await restClient.GetUsersAsync(vendorName);
        }

        public async Task PostVendor(Vendor vendor)
        {
            RestClient<Vendor> restClient = new RestClient<Vendor>();

            await restClient.PostAsync(vendor);
        }

        public async Task<List<Vendor>> GetVendors()
        {
            RestClient<Vendor> restClient = new RestClient<Vendor>();

            var vendorsList = await restClient.GetAsync();

            return vendorsList;
        }
    }
}
