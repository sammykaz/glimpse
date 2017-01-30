using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class TileDetailsViewModel : BaseViewModel
    {
        private List<byte[]> _images;
        private readonly IPromotionImageDataService _promotionImageDataService;
        private int _promotionId; 

        public TileDetailsViewModel(IMvxMessenger messenger, IPromotionImageDataService promotionImageDataService)
        {
            _promotionImageDataService = promotionImageDataService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("PromotionID"))
            {
                _promotionId = Convert.ToInt32((parameters.Data["PromotionID"]));
            }


            base.InitFromBundle(parameters);
        }

        public List<byte[]> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                RaisePropertyChanged(() => Images);
            }
        }

        public async Task<List<byte[]>> GetImageList()
        {
            //getting images for promotion
            _images = await _promotionImageDataService.GetImageListFromPromotionWithLocationId(_promotionId);

            return _images;
        }

       /* public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override Task InitializeAsync()
        {
            return Task.Run(async () =>
            {
               
            });
        }*/
    }
}