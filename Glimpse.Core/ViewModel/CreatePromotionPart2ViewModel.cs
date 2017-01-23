using System;
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
        private readonly IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        private readonly IPromotionImageDataService _promotionImageDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();
        private Categories selectedCategory;

        public CreatePromotionPart2ViewModel(IPromotionDataService promotionDataService, IVendorDataService vendorDataService, IPromotionImageDataService promotionImageDataService)
        {
            _promotionDataService = promotionDataService;
            _vendorDataService = vendorDataService;
            _promotionImageDataService = promotionImageDataService;
            _promotionImageList = new List<byte[]>();
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

        private List<byte[]> _promotionImageList;
        public List<byte[]> PromotionImageList
        {
            get { return _promotionImageList; }
            set
            {
                _promotionImageList = value;
                RaisePropertyChanged(() => PromotionImageList);
            }
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


                    Vendor vendor = await _vendorDataService.SearchVendorByEmail(Settings.Email);

                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],
                        Category = selectedCategory,
                        PromotionStartDate = _promotionStartDate,
                        PromotionEndDate = _promotionEndDate,
                        PromotionImage = Bytes,
                        PromotionImageURL = vendor.VendorId + "/" + dataFromCreatePromotionPart1["PromotionTitle"] + "/" + "cover",
                        VendorId = vendor.VendorId,
                    };                  

                    vendor.Promotions.Add(promotion);

                    await _promotionDataService.StorePromotion(promotion);

                    //this next line is not actually adding promotions, dont know why, works for all other
                    //await _vendorDataService.EditVendor(vendor.VendorId, vendor);
                    List<Promotion> promotions = await _promotionDataService.GetPromotions();

                    //index for unique naming the promotion image
                    int i = 1;
                    foreach(byte[] promotionImage in PromotionImageList)
                    {
                        PromotionImage promotionImageInstance = new PromotionImage()
                        {
                            Image = promotionImage,
                            PromotionId = promotions[promotions.Count - 1].PromotionId,
                            ImageURL = vendor.VendorId + "/" + dataFromCreatePromotionPart1["PromotionTitle"] + "/" + "image" + i
                        };

                        await _promotionImageDataService.StorePromotion(promotionImageInstance);
                        i++;
                    }
                                        
                    ShowViewModel<VendorProfilePageViewModel>(new { index = 0 });

                });
            }
        }
    }
}
