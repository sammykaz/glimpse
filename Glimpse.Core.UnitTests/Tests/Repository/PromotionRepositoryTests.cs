using System;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glimpse.Core.UnitTests.Tests.Repository
{
    [TestClass]
    public class PromotionRepositoryTests
    {
        IPromotionRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new PromotionRepository();
        }

        [TestMethod]
        public async Task Test_GetPromotions_Return_All_Promotions()
        {
            //Act
            var promotions = await repository.GetPromotions();

            //Assert
            Assert.AreNotEqual(0, promotions.Count);
            Assert.AreEqual(promotions[9].CategoriesList, "Footwear,Electronics,Restaurants,Services,Apparel");
        }


        [TestMethod]
        public async Task Test_StorePromotion_Creates_Promotion()
        {
            //arrange
            var promotionsBefore = await repository.GetPromotions();
            var promotionsCountBefore = promotionsBefore.Count;


            Promotion promotion = new Promotion()
            {
                Title = "Test Promotion",
                Description = "PromotionDescription",
                Categories = { Category.Apparel, Category.Electronics },
                PromotionStartDate = "02/04",
                PromotionEndDate = "24/24"
            };

            //act 
            await repository.StorePromotion(promotion);

            //Assert
            var promotonsAfter = await repository.GetPromotions();
            var promotionsCountAfter = promotonsAfter.Count;

            var difference = promotionsCountAfter - promotionsCountBefore;
            Assert.IsTrue(difference == 1);
        }


        [TestMethod]
        public async Task Test_Categories_Match_Selected_Categories()
        {
            //Act
            var promotions = await repository.GetPromotions();

            //Assert
            Assert.AreNotEqual(0, promotions.Count);
            Assert.AreEqual(promotions[9].CategoriesList, "Footwear,Electronics,Restaurants,Services,Apparel");
        }

    }
}
