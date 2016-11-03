using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.UnitTests.Mocks;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class StoreDataServiceTests
    {
        IStoreDataService storeDataService;

        [TestInitialize]
        public void Initialize()
        {
            storeDataService = ServiceMocks.GetMockStoreDataService(3);
        }

        [TestMethod]
        public async Task GetStores_Return_All_Stores()
        {
            var stores = await storeDataService.GetAllStores();

            Assert.AreEqual(3, stores.Count);
        }
    }
}