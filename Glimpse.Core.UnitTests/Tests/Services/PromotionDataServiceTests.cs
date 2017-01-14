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
    public class PromotionDataServiceTests
    {
        private PromotionDataService _pds;

        [TestInitialize]
        public void Initialize()
        {
            PromotionRepository promotionRepo = new PromotionRepository();
            VendorRepository vendorRepo = new VendorRepository();
            _pds = new PromotionDataService(promotionRepo, vendorRepo);
        }

        [TestMethod]
        public async Task GetActivePromotions_Returns_Valid()
        {
            //arrange

            //act
            List<PromotionWithLocation> activePromos = await _pds.GetActivePromotions();
            //assert

            foreach(PromotionWithLocation promo in activePromos)
            {
                Assert.IsTrue(promo.PromotionStartDate < DateTime.Now && promo.PromotionEndDate > DateTime.Now);                   
            }

        }

    }
}
