
using System.Collections.Generic;

namespace Glimpse.Core.Model
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Category> Categories { get; set; }

        public string PromotionStartDate { get; set; }

        public string PromotionEndDate { get; set; }

        public bool PromotionActive { get; set; }

    }

    //Category Types for Promotions
    public enum Category
    {
        Footwear,
        Electronics,
        Jewellery,
        Restaurants,
        Services,
        Apparel
    }

}