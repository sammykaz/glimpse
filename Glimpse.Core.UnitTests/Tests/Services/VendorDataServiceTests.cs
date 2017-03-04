using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Glimpse.Core.Services.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class VendorDataServiceTests
    {
        private VendorDataService _vds;

        private readonly string _testEmail = "unitTestEmail@gmail.com";
        private readonly string _testPassword = "unitTestPassword";
        
        [TestInitialize]
        public void Initialize()
        {
            VendorRepository vendorRepo = new VendorRepository();
            _vds = new VendorDataService(vendorRepo);
        }

        [TestMethod]
        public async Task CreateVendor_DuplicateEmail_Doesnt_Create()
        {
            //arrange
            Vendor vendor = new Vendor
            {
                Email = _testEmail,
                Password = _testPassword,
                CompanyName = "UnitTestCompany",
                Address = "Unit Test Address",
                Telephone = "543-535-5353",
                Location = new Location
                {
                    Lat = 54.434,
                    Lng = 53.656,
                },                
            };


            //act
            await _vds.SignUp(vendor);

            //signup twice
            await _vds.SignUp(vendor);

            //assert
            //check that 2 user with same email were not created

            List<Vendor> allVendors = await _vds.GetVendors();
            List<Vendor> vendorWithTestEmail = allVendors.FindAll(v => v.Email == _testEmail);

            Assert.IsTrue(vendorWithTestEmail.Count == 1);

            //clean up

            await _vds.DeleteVendor(vendorWithTestEmail[0]);
        }


        [TestMethod]
        public async Task CheckIfVendorExists_returns_valid()
        {
            //arrange
            Vendor vendor = new Vendor
            {
                Email = _testEmail,
                Password = _testPassword,
                CompanyName = "UnitTestCompany",
                Address = "Unit Test Address",
                Telephone = "543-535-5353",
                Location = new Location
                {
                    Lat = 54.434,
                    Lng = 53.656,
                },
            };

            //act
            await _vds.SignUp(vendor);

            //assert
            Assert.IsTrue(await _vds.CheckIfVendorExists(_testEmail));

            //clean up

            await _vds.DeleteVendor(vendor);
        }

        [TestMethod]
        public async Task CheckIfPasswordAndSalt_AreNull()
        {
            //arrange
            Vendor vendor = new Vendor
            {
                Email = _testEmail,
                Password = _testPassword,
                CompanyName = "UnitTestCompany",
                Address = "Unit Test Address",
                Telephone = "543-535-5353",
                Location = new Location
                {
                    Lat = 54.434,
                    Lng = 53.656,
                },
            };

            //act
            await _vds.SignUp(vendor);

            //assert
            Vendor vendorFromDb = await _vds.SearchVendorByEmail(_testEmail);
            Assert.IsTrue(await _vds.CheckIfVendorExists(_testEmail));
            Assert.IsNull(vendorFromDb.Password);
            Assert.IsNull(vendorFromDb.Salt);

            //clean up

            await _vds.DeleteVendor(vendorFromDb);
        }



    }
}
