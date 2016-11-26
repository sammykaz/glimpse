using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using Plugin.RestClient;

namespace Glimpse.Core.Contracts.Repository
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetPromotions();
    }
}
