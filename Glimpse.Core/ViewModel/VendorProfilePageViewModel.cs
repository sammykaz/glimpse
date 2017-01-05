using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Newtonsoft.Json;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.ViewModel
{
    public class VendorProfilePageViewModel : BaseViewModel
    {

        private IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        public List<Promotion> _myPromotionList;

        public VendorProfilePageViewModel(IMvxMessenger messenger, IPromotionDataService promotionDataService, IVendorDataService vendorDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
            _promotionDataService = promotionDataService;
            getPromotions.Execute();
        }

        public List<Promotion> PromotionList
        {
            get { return _myPromotionList; }
            set
            {
                _myPromotionList = value;
                RaisePropertyChanged(() => PromotionList);
            }
        }

        public override async void Start()
        {
            PromotionList = await _promotionDataService.GetPromotions();
        }

        public MvxCommand getPromotions
        {
            get
            {
                return new MvxCommand( async() =>
                {
                    //var result = await _promotionDataService.GetPromotions(6);
                    PromotionList = await _promotionDataService.GetPromotions();
                    Vendor vendor = await _vendorDataService.SearchVendorByEmail(Settings.Email);
                    List<Promotion> promotionForVendor = PromotionList.Where(c => c.VendorId == vendor.VendorId).ToList();
                    PromotionList = promotionForVendor;
                });
            }
        }
        
        public IMvxCommand ShowCreatePromotionView { get { return ShowCommand<CreatePromotionViewModel>(); } }

        private MvxCommand ShowCommand<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            return new MvxCommand(() => ShowViewModel<TViewModel>());
        }
    }
}
