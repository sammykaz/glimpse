using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;

namespace MyTrains.Core.ViewModel
{
    public class VendorSignUpViewModel : BaseViewModel
    {
        public VendorSignUpViewModel(IMvxMessenger messenger) : base(messenger)
        {
            
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
                
            }
        }



    }
}