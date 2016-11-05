using System.Collections.Generic;
using Moq;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using Glimpse.Core.Services.Data;

namespace Glimpse.Core.UnitTests.Mocks
{
    public class ServiceMocks
    {
        public static CityDataService GetMockCityDataService(int count)
        {
            var list = new List<City>();

            var mockexpenseRepository = new Mock<ICityRepository>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new City { CityId = count });
            }
            mockexpenseRepository.Setup(m => m.GetAllCities()).ReturnsAsync(list);

            var cityDataService = new CityDataService(mockexpenseRepository.Object);
            return cityDataService;
        }


        public static StoreDataService GetMockStoreDataService(int count)
        {
            var list = new List<Store>();

            var mockStoreRepository = new Mock<IStoreRepository>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new Store { });
            }
            mockStoreRepository.Setup(m => m.GetAllStores()).ReturnsAsync(list);

            var storeDataService = new StoreDataService(mockStoreRepository.Object);
            return storeDataService;
        }

    }
}