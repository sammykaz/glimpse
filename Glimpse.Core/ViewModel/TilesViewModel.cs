using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class TilesViewModel : BaseViewModel
    {
        private string _currentLanguage;
        private List<string> _languages;
        private IPromotionDataService _promotionDataService;

        public TilesViewModel(IMvxMessenger messenger, IPromotionDataService promotionDataService) : base(messenger)
        {
            _promotionDataService = promotionDataService;
        }



        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                RaisePropertyChanged(() => CurrentLanguage);
            }
        }



    }
}