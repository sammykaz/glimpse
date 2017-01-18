using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Platform;
using Glimpse.Core.Helpers;
using Glimpse.Core.Repositories;
using Glimpse.Core.Services.Data;
using Glimpse.Droid.Views;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class MapTests
    {
        private List<PromotionItem> clusterList;
        private PromotionRepository promotionRepo;
        private VendorRepository vendorRepo;
        private VendorDataService vendorDataService;
        private PromotionDataService promotionDataService;
        [TestInitialize]
        public void Initialize()
        {
            promotionRepo = new PromotionRepository();
            vendorRepo = new VendorRepository();
            clusterList = new List<PromotionItem>();
            vendorDataService = new VendorDataService(vendorRepo);
            promotionDataService = new PromotionDataService(promotionRepo, vendorRepo);
        }

        [TestMethod]
        public async Task TestOnePromotionPinOnMapPerVendor()
        {
            List<PromotionWithLocation> activePromotions = await promotionDataService.GetActivePromotions();
            List<Vendor> allVendors = await vendorDataService.GetVendors();

            var uniqueVendors = allVendors.GroupBy(x => new { x.Location.Lat, x.Location.Lng }).Select(g => g.First()).ToList();

            //Print out the pins
            foreach (var v in uniqueVendors)
            {
                //Get promotions for each vendor
                var promotionsList = activePromotions.Where(e => e.VendorId == v.VendorId).ToList();

                //If there are is no current promotion for vendor
                if (promotionsList.Count != 0)
                {
                    clusterList.Add(new PromotionItem(promotionsList, v.Location.Lat, v.Location.Lng));
                }
            }


            //When user clicks on a promotion pin
            List<PromotionItem> currentPromotion = new List<PromotionItem>();
            currentPromotion.Add(clusterList.FirstOrDefault());

            Assert.AreEqual(1, currentPromotion.Count);
        }


    }
}