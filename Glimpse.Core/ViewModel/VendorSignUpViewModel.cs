using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.ViewModel
{
    public class VendorSignUpViewModel : BaseViewModel
    {
        private Vendor vendor;

        private readonly IVendorDataService _vendorDataService;
        private readonly IUserDataService _userDataService;

        public VendorSignUpViewModel(IMvxMessenger messenger, IVendorDataService vendorDataService, IUserDataService userDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
            _userDataService = userDataService;
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                RaisePropertyChanged(() => CompanyName);

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

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);

            }
        }


        private string _businessPhoneNumber;
        public string BusinessPhoneNumber
        {
            get { return _businessPhoneNumber; }
            set
            {
                _businessPhoneNumber = value;
                RaisePropertyChanged(() => BusinessPhoneNumber);

            }
        }



        public MvxCommand SignUpCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {

                    vendor = await _vendorDataService.SearchVendorByEmail(_email);

                    //Check if email exists in db
                    if (vendor != null)
                    {
                        //TODO Display Error message to user, choose another email
                    }
                    else
                    {
                        Vendor newVendor = new Vendor()
                        {
                            CompanyName = _companyName,
                            Email = _email,
                            Password = _password,
                            //Location = _location,
                            Address = Address
                        };

                        Settings.LoginStatus = true;
                        Settings.Email = _email;

                        await _vendorDataService.SignUp(newVendor);

                        ShowViewModel<MapViewModel>();
                    }
                });
            }
        }
    }
}
    