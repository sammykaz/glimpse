using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Glimpse.Core.Model;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace Glimpse.Core.Model
{
    public class Vendor
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string Salt { get; set; }

        public Address Address { get; set; }

        public Telephone Telephone { get; set; }

        private Location _location;
        public Location Location
        {
            get { return _location; }
            set
            {
                _location = Utility.Geocoding.Geocode(Address.ToString());
            }
        }

    }
}