using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.UnitTests.Mocks;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Repository
{
    [TestClass]
    public class StoreRepositoryTests
    {
        IStoreRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = RepositoryMocks.GetMockStoreRepository(3).Object;
        }

        [TestMethod]
        public async Task GetStores_Return_All_Stores()
        {
            var stores = await repository.GetAllStores();

            Assert.AreEqual(3, stores.Count);
        }
    }
}