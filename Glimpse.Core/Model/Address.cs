﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class Address
    {
        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public override string ToString()
        {
            return Country + " " + Province + " " + City + " " + StreetNumber + " " + Street + " " + PostalCode;
        }
    }
}
