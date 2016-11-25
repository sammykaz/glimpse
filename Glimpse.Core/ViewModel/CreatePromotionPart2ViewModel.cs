using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Glimpse.Core.ViewModel
{
    public class CreatePromotionPart2ViewModel : BaseViewModel
    {

        public CreatePromotionPart2ViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }


        Dictionary<string, string> dataFromCreatePromotionPart1 = new Dictionary<string, string>();

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

        private string _foo;
        public string Foo
        {
            get { return _foo; }
            set { _foo = value; RaisePropertyChanged(() => Foo); }
        }
      

    public MvxCommand selectImg
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        // DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                    File = await CrossMedia.Current.PickPhotoAsync();


                    if (File == null)
                        return;

//                    image.Source = image.FromStream(() =>
   //                 {
   //                     var stream = file.GetStream();
    //                    file.Dispose();
    //                    return stream;
     //               });

                });

            }
        }


    }
}
