using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Glimpse.Core.Model;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

using System.Collections.Generic;

namespace Glimpse.Core.Model
{
    public class Vendor
    {
        public Vendor()
        {
            Promotions = new List<Promotion>();
        }
        public int VendorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string Salt { get; set; }

        public Address Address { set; get; }

        public Telephone Telephone { get; set; }

        public Location Location { get; set; }
   
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}