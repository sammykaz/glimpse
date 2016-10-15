using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.UnitTests.Mocks;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class CityDataServiceTests
    {
        ICityDataService cityDataService;

        [TestInitialize]
        public void Initialize()
        {
            cityDataService = ServiceMocks.GetMockCityDataService(3);
        }

        [TestMethod]
        public async Task GetCities_Return_All_Cities()
        {
            var cities = await cityDataService.GetAllCities();

            Assert.AreEqual(3, cities.Count);
        }
    }
}