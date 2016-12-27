using Glimpse.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebServices.Models;

namespace WebServices.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebServices.Models.GlimpseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebServices.Models.GlimpseDbContext context)
        {
            // Tuple<string, string> passwordTuple = Cryptography.EncryptAes("password");
            //string password = passwordTuple.Item1.

            IList<User> users = new List<User>();

            for (int i = 0; i < 100; i++)
            {
                users.Add(new User() {
                    Email = Faker.Internet.Email(),
                    Password = RandomString(8),
                    Salt = "userSalt" });
            }

            IList<Promotion> promotions1 = new List<Promotion>();
            IList<Promotion> promotions2 = new List<Promotion>();
            IList<Promotion> promotions3 = new List<Promotion>();
            IList<Promotion> promotions4 = new List<Promotion>();
            IList<Promotion> promotions5 = new List<Promotion>();
            IList<Promotion> promotions6 = new List<Promotion>();
            IList<Promotion> promotions7 = new List<Promotion>();
            IList<Promotion> promotions8 = new List<Promotion>();
            IList<Promotion> promotions9 = new List<Promotion>();
            IList<Promotion> promotions10 = new List<Promotion>();

           foreach(Promotion promotion in promotions1)
            { 
                promotions1.Add(new Promotion() {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions2)
            {
                promotions2.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions3)
            {
                promotions3.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions4)
            {
                promotions4.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions5)
            {
                promotions5.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions6)
            {
                promotions6.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions7)
            {
                promotions7.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions8)
            {
                promotions8.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions9)
            {
                promotions9.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions10)
            {
                promotions10.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            foreach (Promotion promotion in promotions1)
            {
                promotions1.Add(new Promotion()
                {
                    Title = Faker.Lorem.Words(2).ToString(),
                    Description = Faker.Lorem.Sentence(5),
                    Categories = GetRandomCategory(),
                    PromotionStartDate = new DateTime(2016, 12, 1),
                    PromotionEndDate = GetRandomDate(),
                });
            }

            IList<Vendor> vendors = new List<Vendor>();

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions1
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions2
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions3
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions4
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions5
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions6
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions7
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions8
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions9
            });

            vendors.Add(new Vendor()
            {
                Email = Faker.Internet.Email(),
                Password = RandomString(8),
                Salt = "vendorSalt",
                CompanyName = Faker.Company.Name(),
                Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                Telephone = Faker.Phone.Number(),
                Location = GetRandomLatLon(),
                Promotions = promotions9
            });

            for (int i = 0; i < 100; i++)
            {
                vendors.Add(new Vendor()
                {
                    Email = Faker.Internet.Email(),
                    Password = RandomString(8),
                    Salt = "vendorSalt",
                    CompanyName = Faker.Company.Name(),
                    Address = Faker.Address.Country() + "," + Faker.Address.UsState() + "," + Faker.Address.City() +
                              "," + Faker.Address.StreetSuffix() + "," + Faker.Address.ZipCode(),
                    Telephone = Faker.Phone.Number(),
                    Location = GetRandomLatLon(),
                    Promotions = null
                });
            }


            foreach (Promotion promotion in promotions1)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions2)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions3)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions4)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions5)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions6)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions7)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions8)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions9)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Promotion promotion in promotions10)
            {
                context.Promotions.Add(promotion);
            }

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            foreach (Vendor vendor in vendors)
            {
                context.Vendors.Add(vendor);
            }

            base.Seed(context);

            /*
            var name = Faker.Name.FullName();  // "Alene Hayes"
            Faker.Internet.Email(name);  // "alene_hayes@hartmann.co.uk"
            Faker.Internet.UserName(name);  // "alene.hayes"
            Faker.Internet.Email();  // "morris@friesen.us"
            Faker.Internet.FreeEmail();  // "houston_purdy@yahoo.com"
            Faker.Internet.DomainName();  // "larkinhirthe.com"
            Faker.Phone.Number();  // "(033)216-0058 x0344"

            Faker.Address.StreetAddress();  // "52613 Turcotte Lock"
            Faker.Address.SecondaryAddress();  // "Suite 656"
            Faker.Address.City();  // "South Wavaside"

            Faker.Address.UkCounty();  // "West Glamorgan"
            Faker.Address.UkPostCode().ToUpper();  // "BQ7 3AM"

            Faker.Address.UsState();  // "Tennessee"
            Faker.Address.ZipCode();  // "66363-7828"

            Faker.Company.Name();  // "Dickens Group"
            */

        }




    private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static Location GetRandomLatLon()
        {
                double lat = random.NextDouble() * (360 - 180);
                double lon = random.NextDouble() * (180 - 90);
            return new Location(lat, lon);
        }

        private static Category GetRandomCategory()
        {
            Array values = Enum.GetValues(typeof(Categories));
            Categories category = (Categories)values.GetValue(random.Next(values.Length));
            return new Category(category);
        }

        private static DateTime GetRandomDate()
        {
            DateTime start = new DateTime(2016, 12, 1);
            DateTime end = new DateTime(2017, 31, 1);

            var range = start - end;

            var randTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));

            return end + randTimeSpan;
        }

        private static bool GetRandomBoolean()
        {
            return random.NextDouble() >= 0.5;
        }
    }
}
