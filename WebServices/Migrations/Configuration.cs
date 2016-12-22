namespace WebServices.Migrations
{
    using Glimpse.Core.Services.General;
    using Models;
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
           // Tuple<string, string> passwordTuple = Cryptography.EncryptAes("password");
            //string password = passwordTuple.Item1.


            context.Users.AddOrUpdate(
             x => x.UserId, new User()
             {
                 UserId = 1,                
                 Email = "sam@gmail.com",
                 Password = "password",
                 Salt = "salt"                 
             },
             new User()
             {
                 UserId = 2,               
                 Email = "jo@gmail.com",
                 Password = "password",
                 Salt = "salt"
             },
              new User()
              {
                  UserId = 3,                
                  Email = "eric@gmail.com",
                  Password = "password",
                  Salt = "salt"
              },
               new User()
               {
                   UserId = 4 ,                 
                   Email = "john@gmail.com",
                   Password = "password",
                   Salt = "salt"                  
               },
                new User()
                {
                    UserId = 5,                   
                    Email = "john@gmail.com",
                    Password = "password",
                    Salt = "salt"                   
                }
             );
        }
    }
}
