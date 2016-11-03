using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;

namespace Glimpse.Core.Repositories
{
    class StoreRepository: BaseRepository, IStoreRepository
    {

        private static readonly List<Store> AllStores = new List<Store>();
        


        public async Task<List<Store>> GetAllStores()
        {
            return await Task.FromResult(AllStores);
        }

  

    }
}
