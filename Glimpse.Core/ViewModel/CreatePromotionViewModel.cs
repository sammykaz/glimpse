using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionViewModel : BaseViewModel
    {
        private IMvxMessenger messenger;
        public CreatePromotionViewModel(IMvxMessenger messenger) : base(messenger)
        {

        }

        public MvxCommand ContinueCommand
        {
            get
            {


                return new MvxCommand(() =>
                {
                    var desc = new Dictionary<string, string> {
                        {"PromotionTitle", PromotionTitle},{"PromotionDescription", PromotionDescription}, {"Footwear", FootwearIsChecked.ToString()},
                        {"Electronic", ElectronicIsChecked.ToString() },  {"Jewllery", JewlleryIsChecked.ToString() }, {"Restaurants", RestaurantsIsChecked.ToString() },
                        {"Services", ServicesIsChecked.ToString() }, {"Apparel", ApparelIsChecked.ToString()}
                    };
                                     
                    ShowViewModel<CreatePromotionPart2ViewModel>(desc);
                });
                
            }
        }

        private string _promotionTitle;
        public string PromotionTitle
        {
            get { return _promotionTitle; }
            set
            {
                _promotionTitle = value;
                RaisePropertyChanged(() => PromotionTitle);
            }
        }
        
        private string _promotionDescription;
        public string PromotionDescription
        {
            get { return _promotionDescription; }
            set
            {
                _promotionDescription = value;
                RaisePropertyChanged(() => PromotionDescription);

            }
        }

        private bool _footwearIsChecked;
        public bool FootwearIsChecked
        {
            get { return _footwearIsChecked; }
            set
            {
                _footwearIsChecked = value;
                RaisePropertyChanged(() => FootwearIsChecked);
            }
        }
        private bool _electronicIsChecked;
        public bool ElectronicIsChecked
        {
            get { return _electronicIsChecked; }
            set
            {
                _electronicIsChecked = value;
                RaisePropertyChanged(() => ElectronicIsChecked);
            }
        }
        private bool _jewelleryIsChecked;
        public bool JewlleryIsChecked
        {
            get { return _jewelleryIsChecked; }
            set
            {
                _jewelleryIsChecked = value;
                RaisePropertyChanged(() => JewlleryIsChecked);
            }
        }
        private bool _restaurantsIsChecked;
        public bool RestaurantsIsChecked
        {
            get { return _restaurantsIsChecked; }
            set
            {
                _restaurantsIsChecked = value;
                RaisePropertyChanged(() => RestaurantsIsChecked);
            }
        }
        private bool _servicesIsChecked;
        public bool ServicesIsChecked
        {
            get { return _servicesIsChecked; }
            set
            {
                _servicesIsChecked = value;
                RaisePropertyChanged(() => ServicesIsChecked);
            }
        }
        private bool _apparelIsChecked;
        public bool ApparelIsChecked
        {
            get { return _apparelIsChecked; }
            set
            {
                _apparelIsChecked = value;
                RaisePropertyChanged(() => ApparelIsChecked);
            }
        }


    }
}
