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
    public class PromotionImageDataService : IPromotionImageDataService
    {

        private readonly IPromotionImageRepository promotionImageRepository;       

        public PromotionImageDataService(IPromotionImageRepository promotionImageRepository)
        {
            this.promotionImageRepository = promotionImageRepository;
          
        }

        public async Task<List<PromotionImage>> GetPromotion(int id)
        {
            return await promotionImageRepository.GetPromotionImage(id);
        }

        public async Task<List<PromotionImage>> GetPromotions()
        {
            return await promotionImageRepository.GetPromotionImages();
        }

        public async Task StorePromotion(PromotionImage promotionImage)
        {
            await promotionImageRepository.StorePromotionImage(promotionImage);
        }
    }
}
