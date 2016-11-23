using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;

namespace Glimpse.Core.Repositories
{
    public class StoreRepository: IStoreRepository
    {
        private static readonly List<Store> AllStores = new List<Store>
        {
            new Store()
            {
                Name = "Store",
                Location = new Location { Lat = 45.5017, Lng = -73.5673 }
            }
        };

        public async Task<List<Store>> GetAllStores()
        {
            return await Task.FromResult(AllStores);
        }

  

    }
}
