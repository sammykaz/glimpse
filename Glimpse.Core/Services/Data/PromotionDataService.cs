using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.Services.Data
{
    public class PromotionDataService : IPromotionDataService
    {

        private readonly IPromotionRepository _promotionRepository;

        public async Task<List<Promotion>> GetPromotions(string username)
        {
            return await _promotionRepository.GetPromotion(username);
        }

        public async Task StorePromotion(Promotion promotion)
        {
            await _promotionRepository.StorePromotion(promotion);
        }
    }

}