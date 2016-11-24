using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Contracts.Services
{
    public interface IPromotionDataService
    {
        Task StorePromotion(Promotion promotion);

        Task<List<Promotion>> GetPromotions(string username);
    }
}
