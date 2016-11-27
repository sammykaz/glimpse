using System;
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

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {
        private readonly IPromotionDataService _promotionDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();
   //     private readonly IMvxPictureChooserTask _pictureChooserTask;

        public CreatePromotionPart2ViewModel(IMvxMessenger messenger, IPromotionDataService promotionDataService) : base(messenger)
        {
            _promotionDataService = promotionDataService;
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
        private string _promotionStartDate;
        public string PromotionStartDate
        {
            get { return _promotionStartDate; }
            set { _promotionStartDate = value; RaisePropertyChanged(() => PromotionStartDate); }
        }

        private string _promotionEndDate;
        public string PromotionEndDate
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
                    List<Category> promotionCategories = new List<Category> { };

                    foreach (string key in dataFromCreatePromotionPart1.Keys)
                    {
                        if (dataFromCreatePromotionPart1[key] == "True")
                            promotionCategories.Add((Category)Enum.Parse(typeof(Category), key, true));
                    }

                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],
                        Categories = promotionCategories,
                        PromotionStartDate = _promotionStartDate,
                        PromotionEndDate = _promotionEndDate,
                       // PromotionImage = File,
                       // PromotionLength = SelectedLengthOfThePromotion
                    };
                    try
                    {
                        await _promotionDataService.StorePromotion(promotion);
                    }
                    catch(Exception e)
                    {
                        
                    }
                    ShowViewModel<VendorProfilePageViewModel>();
                });
            }
        }
    }
}
