using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;
using Plugin.RestClient;

namespace Glimpse.Core.Repositories
{
    public class VendorRepository : BaseRepository, IVendorRepository
    {
        public async Task<User> SearchUser(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string userName, string password)
        {
            throw new NotImplementedException();
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
