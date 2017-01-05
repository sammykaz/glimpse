using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class PromotionWithLocation
    {

        public int VendorId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }      

        public Category Categories { get; set; }

        public string CompanyName { get; set; }

        public int Duration { get; set; }

        public byte[] Image { get; set; }


        public Location Location { get; set; }

     //   public byte[] PromotionImage { get; set; }

    }
}
