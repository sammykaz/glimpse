﻿using Glimpse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Contracts.Services
{
    public interface IPromotionImageDataService
    {

        Task StorePromotion(PromotionImage promotionImage);

        Task<List<PromotionImage>> GetPromotion(int id);

        Task<List<PromotionImage>> GetPromotions();
    }
}
