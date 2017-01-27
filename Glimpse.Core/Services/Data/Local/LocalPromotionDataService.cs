using Glimpse.Core.Contracts.Services;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Core.Model;
using MvvmCross.Platform;
using SQLite.Net.Async;

namespace Glimpse.Core.Services.Data.Local
{
    public class LocalPromotionDataService : ILocalPromotionDataService
    {
        




        public LocalPromotionDataService()
        {

        }


        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Delete(PromotionWithLocation promotionWithLocation)
        {
            throw new NotImplementedException();
        }

        public void Insert(PromotionWithLocation promotionWithLocation)
        {
            throw new NotImplementedException();
        }
    }
}
