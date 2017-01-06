
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServices.Models
{
    public class Promotion
    {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionId { get; set; }      

        public string Title { get; set; }

        public string Description { get; set; }

        public Categories Category { get; set; }

        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }
 
        public byte[] PromotionImage { get; set;}

        public virtual Vendor Vendor { get; set; }

        [Key, Column(Order = 2)]
        public virtual int VendorId { get; set; }

    }

    public enum Categories
    {
        Footwear,
        Electronics,
        Jewellery,
        Restaurants,
        Services,
        Apparel
    }
   
}