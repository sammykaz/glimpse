using Glimpse.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Core.Contracts.Services
{
    public interface  IStoreDataService
    {
        Task<List<Store>> GetAllStores();
    }
}
