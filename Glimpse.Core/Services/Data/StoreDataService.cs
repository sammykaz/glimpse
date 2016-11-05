using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;

namespace Glimpse.Core.Services.Data
{
    public class StoreDataService : IStoreDataService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreDataService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }


        public virtual async Task<List<Store>> GetAllStores()
        {
            return await _storeRepository.GetAllStores();
        }

    }
}