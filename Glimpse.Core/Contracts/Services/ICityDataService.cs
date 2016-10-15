using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface ICityDataService
    {
        Task<List<City>> GetAllCities();

        Task<City> GetCityById(int cityId);
    }
}