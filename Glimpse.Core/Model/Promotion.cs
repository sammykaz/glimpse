
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class Promotion : MvxNotifyPropertyChanged
    {
        public string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        private List<Category> _categories = new List<Category>();
        public List<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (!Category.IsDefined(typeof(Category), _categories))
                {
                    _categories = null;
                }

                _categories = value;
            }
        }

        //These dates will be extracted from a calendar UI in the future.
        public string PromotionStartDate { get; set; }

        public string PromotionEndDate { get; set; }

        public bool PromotionActive;


        //Add images here

        //Any promotion logic below

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
