﻿using System;
using System.Collections.Generic;
using Glimpse.Core.Model;
using System.IO;
using Glimpse.Core.Contracts.Services;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {
        private readonly IPromotionDataService promotionDataService;
        private IVendorDataService vendorDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();
        private Categories selectedCategory;

        public CreatePromotionPart2ViewModel(IPromotionDataService promotionDataService, IVendorDataService vendorDataService)
        {
            this.promotionDataService = promotionDataService;
            this.vendorDataService = vendorDataService;
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
                    foreach (string key in dataFromCreatePromotionPart1.Keys)
                    {
                        if (dataFromCreatePromotionPart1[key] == "True")
                            selectedCategory = (Categories) Enum.Parse(typeof(Categories), key, true);
                    }

                    //Calculate DateTime span
                    //TimeSpan promotionLength = _promotionEndDate - _promotionStartDate;
                    Vendor vendor = await vendorDataService.SearchVendorByEmail(Settings.Email);

                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],
                        Category = selectedCategory,
                        PromotionStartDate = _promotionStartDate,
                        PromotionEndDate = _promotionEndDate,
                        PromotionImage = Bytes,
                        VendorId = vendor.VendorId
                    };

                   


                    //TODO Fix how pomotion gets added



                    await promotionDataService.StorePromotion(promotion);
                    //vendor.Promotions.Add(promotion);
                    //vendor.CompanyName = "modified ";

                    //await vendorDataService.EditVendor(vendor.VendorId, vendor);
                   
                    ShowViewModel<MapViewModel>();
                });
            }
        }
    }
}
