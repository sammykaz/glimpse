using Glimpse.Core.Model;
using Glimpse.Core.Repositories;
using Glimpse.Core.Services.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.UnitTests.Tests.Services
{
    [TestClass]
    public class PromotionDataServiceTests
    {
        private PromotionDataService pds;

        [TestInitialize]
        public void Initialize()
        {
            pds = new PromotionDataService(new PromotionRepository(), new VendorRepository());

        }

        [TestMethod]
        public void AddPromotion_Successful()
        {
            var list = pds.GetActivePromotions();

            Vendor newVendor = new Vendor()
            {
                CompanyName = "Unit Test Vendor",
                Email = "unitTestVendor@gmail.com",
                Telephone = "514-643-5435",
                Password = "password",
                Location = new Location(20,20),
                Address = "Unit Test Address"
            };



            Promotion promotion = new Promotion()
            {
                Title = "Unit Test Title",
                Description = "Unit Test Description",
                Category = Categories.Restaurants,
                PromotionStartDate = DateTime.Now,
                PromotionEndDate = DateTime.Now.AddDays(3),
                PromotionImage = ConvertImageToByteArray(UnitTests.Properties.Resources.pic0),
            };

            //vendor.Promotions.Add(promotion);
        }

        public byte[] ConvertImageToByteArray(Bitmap image)
        {
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Bmp);

                data = memoryStream.ToArray();
            }

            return data;
        }

    }

}
