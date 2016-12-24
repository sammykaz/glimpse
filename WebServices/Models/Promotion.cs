
using System;
using System.Collections.Generic;

namespace WebServices.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }

        public string Title;       

        public string Description;       

        public int? VendorId { get; set; }

        public Category Categories { get; set; }     

        //These dates will be extracted from a calendar UI in the future.
        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }

        public bool PromotionActive { get; set; }


        //Add images here

        //Any promotion logic below

    }

}