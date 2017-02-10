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
        private string _promotionTitle;
        private string _promotionDuration;
        private string _promotionDescription;

        public TileDetailsViewModel(IMvxMessenger messenger, IPromotionImageDataService promotionImageDataService)
        {
            _promotionImageDataService = promotionImageDataService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            if (parameters.Data.ContainsKey("PromotionID"))
                _promotionId = Convert.ToInt32((parameters.Data["PromotionID"]));

            if (parameters.Data.ContainsKey("PromotionTitle"))
                _promotionTitle = (parameters.Data["PromotionTitle"]);

            if (parameters.Data.ContainsKey("PromotionDuration"))
                _promotionDuration = (parameters.Data["PromotionDuration"]);

            if (parameters.Data.ContainsKey("PromotionDescription"))
                _promotionDescription = (parameters.Data["PromotionDescription"]);


            base.InitFromBundle(parameters);
        }

        public string PromotionTitle
        {
            get
            {
                if (_promotionTitle == null)
                    _promotionTitle = "";

                return _promotionTitle;
            }
        }
        public string PromotionDuration
        {
            get
            {
                if (_promotionDuration == null)
                    _promotionDuration = "";

                return ConvertSecondsToMinutes(_promotionDuration);
            }
        }

        public string PromotionDescription
        {
            get
            {
                if (_promotionDescription == null)
                    _promotionDescription = "";

                return _promotionDescription;
            }
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

        private string ConvertSecondsToMinutes(string value)
        {

            TimeSpan timespan = TimeSpan.FromSeconds(Convert.ToInt32(value));
            int totalMins = (int)timespan.TotalMinutes;
            string displayTime = Convert.ToString(totalMins);

            if (totalMins == 1)
                displayTime = displayTime + " minute away!";
            else
                displayTime = displayTime + " minutes away!";

            return displayTime;
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