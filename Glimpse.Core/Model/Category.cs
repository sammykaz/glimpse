﻿
namespace Glimpse.Core.Model
{
    public class Category
    {
        public Category(Categories categories)
        {
            Categories = categories;
        }

        public int CategoryId { get; set; }

        public Categories Categories { get; set; }
    }
}