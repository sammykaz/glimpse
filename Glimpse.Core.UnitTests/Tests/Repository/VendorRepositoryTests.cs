using System;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glimpse.Core.UnitTests.Tests.Repository
{
    [TestClass]
    public class VendorRepositoryTests
    {
        IVendorRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new VendorRepository(); ;
        }

        [TestMethod]
        public async Task Test_GetVendors_Return_All_Users()
        {
            //Act
            var vendors = await repository.GetVendors();

            //Assert
            Assert.AreNotEqual(0, vendors.Count);
        }

        [TestMethod]
        public async Task Test_PostVendors_Creates_Vendor()
        {
            //arrange
            var vendorsBefore = await repository.GetVendors();
            var vendorsCountBefore = vendorsBefore.Count;
            Address address = new Address();
            address.Country = "Canada";
            address.Province = "Quebec";
            address.City = "Montreal";
            address.StreetNumber = "104";
            address.Street = "Kingston";
            address.PostalCode = "h3z4k1";

            Telephone telephone = new Telephone();
            telephone.BusinessPhoneNumber = "514-543-6363";
            telephone.PersonalPhoneNumber = "514-852-8542";

            Vendor vendor = new Vendor
            {
                FirstName = "Joseph",
                LastName = "Boolster",
                UserName = "JoJo123",
                Email = "jojo@gmail.com",
                Password = "mypassword",
                Address = address,
                Telephone = telephone,
                CompanyName = "Boolmeister",
                Salt = "salt"
            };

            //act 
            await repository.PostVendor(vendor);

            //Assert
            var vendorsAfter = await repository.GetVendors();
            var vendorsCountAfter = vendorsAfter.Count;

            var difference = vendorsCountAfter - vendorsCountBefore;
            Assert.IsTrue(difference == 1);
        }
    }
}
