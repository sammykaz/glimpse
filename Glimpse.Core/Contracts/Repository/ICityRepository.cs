using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Repository
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllCities();

        Task<City> GetCityById(int cityId);
    }
}
