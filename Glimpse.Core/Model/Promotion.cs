
//using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Core.Model
{
    public class Promotion
    {
        public string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int VendorId { get; set; }

        public List<Category> Categories { get; set; }

        public string CategoriesList
        {
            get { return String.Join(",", Categories); }

            set { Categories = value.Split(',').Select(x => (Category)Enum.Parse(typeof(Category), x)).ToList(); }
        }

        //These dates will be extracted from a calendar UI in the future.
        public string PromotionStartDate { get; set; }

        public string PromotionEndDate { get; set; }

        public string PromotionLength { get; set; }

        public bool PromotionActive { get; set; }
 
        //public MediaFile PromotionImage { get; set;}

        //Add images here

        //Any promotion logic below

    }

}