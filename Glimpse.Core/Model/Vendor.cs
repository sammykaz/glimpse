
using System.Collections.Generic;

namespace Glimpse.Core.Model
{
    public class Vendor
    {
        //Vendor Sign up information

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string Company { get; set; }

        //Vendor added information

        public List<Promotion> Promotions { get; set; }

        
        private Location _location;
        public Location Location
        {
            get { return _location; }
            set
            {
                Location temp;
                _location = Utility.Geocoding.Geocode(Address , out temp); }
            }

        //For crypto

        public string Salt { get; set; }
    }
}
