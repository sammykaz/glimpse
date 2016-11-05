using System.Collections.Generic;
using Moq;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;

namespace Glimpse.Core.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<ICityRepository> GetMockCityRepository(int count)
        {
            var list = new List<City>();
            var mockCityRepository = new Mock<ICityRepository>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new City { CityId = count });
            }

            mockCityRepository.Setup(m => m.GetAllCities()).ReturnsAsync(list);
            mockCityRepository.Setup(m => m.GetCityById(It.IsAny<int>())).ReturnsAsync(list[0]);
            return mockCityRepository;
        }

        public static Mock<IStoreRepository> GetMockStoreRepository(int count)
        {
            var list = new List<Store>();
            var mockStoreRepository = new Mock<IStoreRepository>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new Store { });
            }

            mockStoreRepository.Setup(m => m.GetAllStores()).ReturnsAsync(list);
         
            return mockStoreRepository;
        }
    }
}