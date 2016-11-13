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

    }
}
