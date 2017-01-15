using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Glimpse.Core.Services.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class PromotionClicksDataServiceTests
    {
        private PromotionClickDataService _pcds;

        [TestInitialize]
        public void Initialize()
        {
            PromotionClickRepository promoClickRepo = new PromotionClickRepository();
            _pcds = new PromotionClickDataService(promoClickRepo);
        }

        [TestMethod]
        public async Task CreatePromotionClick_Successful()
        {
            //arrange
            PromotionClick promoClick = new PromotionClick
            {
                PromotionId = 1132,
                Time = DateTime.Now
            };

            var allPromoClicksBefore = await _pcds.GetPromotionClicks();
            int countBefore = allPromoClicksBefore.Count;

            //act
            await _pcds.StorePromotionClick(promoClick);

            //assert

            var allPromoClicksAfter = await _pcds.GetPromotionClicks();
            int countAfter = allPromoClicksAfter.Count;

            Assert.AreEqual(1, countAfter - countBefore);

            //cleanup

            await _pcds.DeletePromotionClick(allPromoClicksAfter[countAfter-1]);
        }
    }
}
