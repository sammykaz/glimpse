﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
//using Plugin.Media;
//using Plugin.Media.Abstractions;
using Glimpse.Core.Model;
using Glimpse.Core.Contracts.Repository;
//using MvvmCross.Plugins.PictureChooser;
using System.IO;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.General;


namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {
        private readonly IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();
   //     private readonly IMvxPictureChooserTask _pictureChooserTask;

        public CreatePromotionPart2ViewModel(IMvxMessenger messenger, IPromotionDataService promotionDataService, IVendorDataService vendorDataService) : base(messenger)
        {
            _promotionDataService = promotionDataService;
            _vendorDataService = vendorDataService;
 //           _pictureChooserTask = pictureChooserTask;
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


        private List<String> _lengthOfThePromotion = new List<String>()
            {
                "1","2","3","4","5","6","7"
            };

        public List<String> LengthOfThePromotion
        {
            get { return _lengthOfThePromotion; }
            set { _lengthOfThePromotion = value; RaisePropertyChanged(() => LengthOfThePromotion); }
        }

        private String _selectedLenghtOfThePromotion;
        public String SelectedLengthOfThePromotion
        {
            get { return _selectedLenghtOfThePromotion; }
            set { _selectedLenghtOfThePromotion = value; RaisePropertyChanged(() => SelectedLengthOfThePromotion); }
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
                    

                    List<Categories> promotionCategories = new List<Categories> { };

                    foreach (string key in dataFromCreatePromotionPart1.Keys)
                    {
                        if (dataFromCreatePromotionPart1[key] == "True")
                            promotionCategories.Add((Categories)Enum.Parse(typeof(Categories), key, true));
                    }

                    //var test = await _vendorDataService.GetVendorId(Settings.UserName);
                    //var test2 = false;

                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],

                        //Must fix this part, problems when storing enums, because currently it is being stored as a List of Category objects. Not Compatible.

                        //Categories = promotionCategories,
                        Categories = new List<Category>(),



                        PromotionStartDate = _promotionStartDate,
                        PromotionEndDate = _promotionEndDate,
                        VendorId = await _vendorDataService.GetVendorId(Settings.Email)
                        // PromotionImage = File,
                        // PromotionLength = SelectedLengthOfThePromotion
                    };

                    await _promotionDataService.StorePromotion(promotion);

                    ShowViewModel<VendorProfilePageViewModel>();
                    
                });
            }
        }
    }
}
