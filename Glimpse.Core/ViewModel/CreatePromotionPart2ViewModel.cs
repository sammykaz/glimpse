using System;
using System.Collections.Generic;
using Glimpse.Core.Model;
using System.IO;
using Glimpse.Core.Contracts.Services;
using MvvmCross.Core.ViewModels;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {
        private readonly IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();

        public CreatePromotionPart2ViewModel(IPromotionDataService promotionDataService, IVendorDataService vendorDataService)
        {
            _promotionDataService = promotionDataService;
            _vendorDataService = vendorDataService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            var myPara = parameters;

            foreach (string key in parameters.Data.Keys)
            {
                dataFromCreatePromotionPart1.Add(key, parameters.Data[key]);
            }
            base.InitFromBundle(parameters);
        }
        private DateTime _promotionStartDate;
        public DateTime PromotionStartDate
        {
            get { return _promotionStartDate; }
            set { _promotionStartDate = value; RaisePropertyChanged(() => PromotionStartDate); }
        }

        private DateTime _promotionEndDate;
        public DateTime PromotionEndDate
        {
            get { return _promotionEndDate; }
            set { _promotionEndDate = value; RaisePropertyChanged(() => PromotionEndDate); }
        }


        private byte[] _bytes;
        public byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; RaisePropertyChanged(() => Bytes); }
        }

        private void OnPicture(Stream pictureStream)
        {
            var memoryStream = new MemoryStream();
            pictureStream.CopyTo(memoryStream);
            Bytes = memoryStream.ToArray();
        }

        /* public MvxCommand selectImg
         {
             get
             {
                 return new MvxCommand(() =>
                 {
                     _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
                 });
             }
         }
         */
        public MvxCommand createPromotion
        {
            get
            {
                return new MvxCommand(async () =>
                {


                    List<Category> promotionCategories = new List<Category> { };

                    foreach (string key in dataFromCreatePromotionPart1.Keys)
                    {
                        if (dataFromCreatePromotionPart1[key] == "True")
                            promotionCategories.Add(new Category((Categories)Enum.Parse(typeof(Categories), key, true)));
                    }

                    //Calculate DateTime span
                    //TimeSpan promotionLength = _promotionEndDate - _promotionStartDate;


                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],
                        Categories = promotionCategories,
                        PromotionStartDate = _promotionStartDate,
                        PromotionEndDate = _promotionEndDate,
                        //PromotionImage = File,
                    };

                    await _promotionDataService.StorePromotion(promotion);

                    ShowViewModel<VendorProfilePageViewModel>();

                });
            }
        }
    }
}
