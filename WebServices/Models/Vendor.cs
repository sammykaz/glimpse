using System.Collections;
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

        public Address Address { get; set; }

        public Telephone Telephone { get; set; }

        public Location Location { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }

        public bool IsVendor { get; set; }
    }
}