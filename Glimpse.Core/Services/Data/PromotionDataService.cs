using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Services.Data
{
    public class PromotionDataService : IPromotionDataService
    {
        private readonly IPromotionRepository _promotionRepository;
       
        public PromotionDataService(IPromotionRepository promotionDataRepository)
        {
            _promotionRepository = promotionDataRepository;
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            return await _promotionRepository.GetPromotions();
        }
    }
}
