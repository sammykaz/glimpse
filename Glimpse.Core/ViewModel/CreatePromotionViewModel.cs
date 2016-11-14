using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionViewModel : BaseViewModel
    {
        public CreatePromotionViewModel(IMvxMessenger messenger) : base(messenger)
        {

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
        public bool FootwearIsCheck
        {
            get { return _footwearIsChecked; }
            set
            {
                _footwearIsChecked = value;
                RaisePropertyChanged(() => FootwearIsCheck);
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
