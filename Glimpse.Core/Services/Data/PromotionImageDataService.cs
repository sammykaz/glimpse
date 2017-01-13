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

        public async Task<List<PromotionImage>> GetPromotionImage(int id)
        {
            return await promotionImageRepository.GetPromotionImage(id);
        }

        public async Task<List<byte[]>> GetImageListFromPromotionImageId(int id)
        {
            List<PromotionImage> allPromotionImages = await GetPromotionImages();
            List<byte[]> listofImages = allPromotionImages.Where(x => (x.PromotionId == id)).Select(x => x.Image).ToList();

            return listofImages;
        }

        public async Task<List<PromotionImage>> GetPromotionImages()
        {
            return await promotionImageRepository.GetPromotionImages();
        }

        public async Task StorePromotion(PromotionImage promotionImage)
        {
            await promotionImageRepository.StorePromotionImage(promotionImage);
        }
    }
}
