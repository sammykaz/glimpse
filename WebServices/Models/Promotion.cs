﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Glimpse.Core.Model
{
    public class Promotion
    {
        public Promotion()
        {
            Categories = new List<Category>();
        }
        public int PromotionId { get; set; }

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

        public virtual ICollection<Category> Categories { get; set; }

        /*
        public string CategoriesList
        {
            get { return String.Join(",", Categories); }

            set { Categories = value.Split(',').Select(x => (Category)Enum.Parse(typeof(Category), x)).ToList(); }
        }
        */

        //These dates will be extracted from a calendar UI in the future.
        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }

        public bool PromotionActive { get; set; }


        //Add images here

        //Any promotion logic below

    }

}