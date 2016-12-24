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

            IList<Promotion> promotions = new List<Promotion>();

            for (int i = 0; i < 100; i++)
            {
                    promotions.Add(new Promotion() {
                        Title = Faker.Lorem.Words(2).ToString(),
                        Description = Faker.Lorem.Sentence(5),
                        Categories = GetRandomCategory(),
                        PromotionStartDate = new DateTime(2016, 12, 1),
                        PromotionEndDate = GetRandomDate(),
                        PromotionActive = GetRandomBoolean()
                });
            }

            IList<Vendor> vendors = new List<Vendor>();

            for (int i = 0; i < 100; i++)
            {
                vendors.Add(new Vendor()
                {
                    Email = Faker.Internet.Email(),
                    Password = RandomString(8),
                    Salt = "vendorSalt",
                    CompanyName = Faker.Company.Name(),
                    Address = new Address()
                    {
                        Country = Faker.Address.Country(),
                        Province = Faker.Address.UsState(),
                        City = Faker.Address.City(),
                        StreetNumber = Faker.Address.StreetSuffix(),
                        PostalCode = Faker.Address.ZipCode(),

                    }
                    

                });
            }

            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            foreach (Promotion promotion in promotions)
            {
                context.Promotions.Add(promotion);
            }

            foreach (Vendor vendor in vendors)
            {
                context.Vendors.Add(vendor);
            }

            base.Seed(context);


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


        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
