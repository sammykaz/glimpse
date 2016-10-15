using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;

namespace Glimpse.Core.Services.Data
{
    public class CityDataService: ICityDataService
    {
        private readonly ICityRepository _cityRepository;
        public CityDataService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public virtual async Task<List<City>> GetAllCities()
        {
            return await _cityRepository.GetAllCities();
        }

        public async Task<City> GetCityById(int cityId)
        {
            return await _cityRepository.GetCityById(cityId);
        }
    }
}