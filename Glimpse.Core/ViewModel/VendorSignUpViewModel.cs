using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;

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


        private string _company;
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                RaisePropertyChanged(() => Company);

            }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);

            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public MvxCommand SignUpCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    var user = new Vendor(_firstName, _company, _email, _password);
                    int x = 0;
                });
            }
        }


    }
}