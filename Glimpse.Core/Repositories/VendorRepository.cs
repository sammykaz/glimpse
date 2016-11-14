using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;
using Plugin.RestClient;
using static Glimpse.Core.Model.Category;

namespace Glimpse.Core.Repositories
{
    public class VendorRepository : IVendorRepository
    {

        private static readonly List<Vendor> allVendors = new List<Vendor>
        {
            new Vendor(){

                FirstName = "Bob",
                Email = "bob@yahoo.com",
                Address = "123 StreetName",
                Company = "H&M",
                Promotions = new List<Promotion>(){new Promotion(){
                                                Title = "Buy 1 pants get the 2nd 50% off!",
                                                Description = "Promotion description",
                                                Categories = Category.Apparel,
                                                PromotionStartDate = "11/12/2016",
                                                PromotionEndDate = "11/13/2016",
                                                PromotionActive = true,}}}

        };


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
            /*RestClient<Vendor> restClient = new RestClient<Vendor>();

            var vendorsList = await restClient.GetAsync();

            return vendorsList;*/

            return await Task.FromResult(allVendors.Where(x => x.Promotions.Any()).ToList());
        }
    }
}
