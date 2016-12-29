
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServices.Models
{

    public enum Categories
    {
        Footwear,
        Electronics,
        Jewellery,
        Restaurants,
        Services,
        Apparel
    }

    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Categories Category { get; set; }

        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }
 
        public byte[] PromotionImage { get; set;}


    }

}