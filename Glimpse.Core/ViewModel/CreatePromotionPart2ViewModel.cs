using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Glimpse.Core.Model;
using Glimpse.Core.Contracts.Repository;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {
        private readonly IPromotionRepository _promotionDataService;
        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();

        public CreatePromotionPart2ViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
           // var mykey1value = parameters.Data["key1"];
           
            var myPara = parameters;
            var service = myPara.Data["ServicesIsChecked"];
            
            foreach (string key in parameters.Data.Keys)
            {
                dataFromCreatePromotionPart1.Add(key, parameters.Data[key]);
            }
            // And so on
            int i = 0;
            base.InitFromBundle(parameters);
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

        private MediaFile _file;
        public MediaFile File
        {
            get { return _file; }
            set { _file = value; RaisePropertyChanged(() => File); }
        }

        public MvxCommand selectImg
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    List<Category> promotionCategories = new List<Category> { };

                    foreach (string key in dataFromCreatePromotionPart1.Keys)
                    {
                        if (dataFromCreatePromotionPart1[key] == "true")
                            promotionCategories.Add((Category)Enum.Parse(typeof(Category), key, true));
                    }

                    Promotion promotion = new Promotion()
                    {
                        Title = dataFromCreatePromotionPart1["PromotionTitle"],
                        Description = dataFromCreatePromotionPart1["PromotionDescription"],
                        Categories = promotionCategories,
                        PromotionImage = File
                    };
                    var x = 5; //break point to test
                    await _promotionDataService.StorePromotion(promotion);

                    ShowViewModel<MapViewModel>();
                });
            }
        }
    }
}
