﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Core.Model;
using System.Collections.Generic;


namespace Glimpse.Core.Contracts.Repository
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetPromotion(string username);

        Task StorePromotion(Promotion promotion);
    }
}