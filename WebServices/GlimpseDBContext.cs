
using System.Data.Entity;
using Glimpse.Core.Model;

namespace WebServices.Models
{
    public class GlimpseDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GlimpseDBContext() : base("name=DbContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Glimpse.Core.Model.Vendor> Vendors { get; set; }
    }
}
