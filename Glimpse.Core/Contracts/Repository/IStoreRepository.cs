using Glimpse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStores();

    }
}
