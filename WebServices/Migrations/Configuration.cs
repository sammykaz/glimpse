namespace WebServices.Migrations
{
    using Glimpse.Core.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebServices.Models.GlimpseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebServices.Models.GlimpseDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Users.AddOrUpdate(
              x => x.UserId, new User()
              {
                  UserId = 1,
                  FirstName = "sam",
                  LastName = "kaz",
                  Email = "sam@gmail.com",
                  Password = "password",
                  Salt = "salt",
                  IsVendor = false
              },
              new User()
              {
                  UserId = 2,
                  FirstName = "jo",
                  LastName = "bool",
                  Email = "jo@gmail.com",
                  Password = "password",
                  Salt = "salt",
                  IsVendor = false
              },
               new User()
               {
                   UserId = 3,
                   FirstName = "eric",
                   LastName = "leon",
                   Email = "eric@gmail.com",
                   Password = "password",
                   Salt = "salt",
                   IsVendor = false
               },
                new User()
                {
                    UserId = 4,
                    FirstName = "john",
                    LastName = "doe",
                    Email = "john@gmail.com",
                    Password = "password",
                    Salt = "salt",
                    IsVendor = false
                },
                 new User()
                 {
                     UserId = 5,
                     FirstName = "john",
                     LastName = "smith",
                     Email = "john@gmail.com",
                     Password = "password",
                     Salt = "salt",
                     IsVendor = false
                 }
              );


            context.Vendors.AddOrUpdate(
                 x => x.VendorId, new Vendor()
                 {
                     VendorId = 1,
                     FirstName = "sam",
                     LastName = "kaz",
                     Email = "sam@gmail.com",
                     Password = "password",
                     CompanyName = "sam's Company",
                     Salt = "salt",
                     Address = new Address()
                     {
                         Country = "Canada",
                         Province = "Quebec",
                         City = "Montreal",
                         PostalCode = "H4X1C2",
                         Street = "7140",
                         StreetNumber = "Sherbrooke O"
                     },
                     Telephone = new Telephone()
                     {
                         PersonalPhoneNumber = "51454363654",
                         BusinessPhoneNumber = "51454363624"
                     },
                     Location = new Location(45.458151, -73.640116),
                     IsVendor = false
                 });
        }
    }
}
