using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServices.Models
{
    public class PromotionImage
    {
        [Key]
        public int PromotionImageId { get; set; }

        public string ImageURL { get; set; }

        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}

