using Glimpse.Core.DataGenerator;

namespace WebServices.Migrations
{
    using Glimpse.Core.Services.General;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebServices.Models.GlimpseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }       


        protected override void Seed(WebServices.Models.GlimpseDbContext context)
        {

          //  IList<Vendor> vendors = generateVendors();

            IList<Promotion> promotions = DataGenerator.GeneratePromotions(50, context.Vendors.Select(vendor => vendor.VendorId).ToList());

          //      foreach (Vendor vendor in vendors)
          //         context.Vendors.Add(vendor);


            foreach (Promotion promotion in promotions)
                context.Promotions.Add(promotion);
            

            base.Seed(context); 
        }
        
    }
}
