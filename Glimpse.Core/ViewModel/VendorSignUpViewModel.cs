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


        private Location _location;
        public Location Location
        {
            get
            {
                if (_location == null)
                    _location = new Location();

                return _location;
            }
            set
            {
                _location = value;
                RaisePropertyChanged(() => Location);
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


        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        public MvxCommand SignUpCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    //Check if email exists in db
                    if (await _vendorDataService.CheckIfVendorExists(Email))
                    {
                        ErrorMessage = "This email: "+Email+" is already being used by another vendor";
                    }
                    else
                    {
                        Vendor newVendor = new Vendor()
                        {
                            CompanyName = _companyName,
                            Email = _email,
                            Telephone = _businessPhoneNumber,
                            Password = _password,
                            Location = _location,
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
    