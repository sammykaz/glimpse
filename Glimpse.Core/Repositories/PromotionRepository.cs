using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using Plugin.RestClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Core.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        public async Task<List<Promotion>> GetPromotions()
        {
            RestClient<Promotion> restClient = new RestClient<Promotion>();

            var promotionsList = await restClient.GetAsync();

            return promotionsList;
        }
    }
}
