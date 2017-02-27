using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Glimpse.Core.Services.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class LoginDataServiceTests
    {
        private LoginDataService _vds;
        

        [TestInitialize]
        public void Initialize()
        {
            VendorRepository vendorRepo = new VendorRepository();
            _vds = new LoginDataService(vendorRepo);
        }      

        [TestMethod]
        public async Task ValidAuthetication_ReturnTrue()
        {
            Vendor vendor = new Vendor
            {
                Email = "hash@gmail.com",
                Password = "hash"
            };

            RestClient<Vendor> restclient = new RestClient<Vendor>();

            var response =  await restclient.Authenticate(vendor);

            Assert.IsTrue(response);
        
        

        }



    }
}
