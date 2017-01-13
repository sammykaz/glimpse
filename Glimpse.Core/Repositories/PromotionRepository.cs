using Glimpse.Core.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Core.Model;
using Plugin.RestClient;

namespace Glimpse.Core.Repositories
{
    class PromotionRepository : IPromotionRepository
    {

        public async Task StorePromotion(Promotion promotion)
        {
            RestClient<Promotion> restClient = new RestClient<Promotion>();

            await restClient.PostAsync(promotion);
        }

        public async Task<List<Promotion>> GetPromotion(int id)
        {
            RestClient<Promotion> restClient = new RestClient<Promotion>();

            var promotion = await restClient.GetByIdAsync(id);

            return promotion;
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            RestClient<Promotion> restClient = new RestClient<Promotion>();

            var promotions = await restClient.GetAsync();

            return promotions;
        }

        public async Task<List<Promotion>> GetPromotionsByCategory(Categories category)
        {
            RestClient<Promotion> restClient = new RestClient<Promotion>();

            string enumAsString = category.ToString();

            var promotions = await restClient.GetWithFilter(enumAsString);

            return promotions;
        }
    }
}