using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Plugin.RestClient;
using Plugin.Settings.Abstractions;

namespace Glimpse.Core.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IVendorDataService _vendorDataService;
        private readonly IUserDataService _userDataService;
        private readonly ILoginDataService _loginDataService;
        private string _email;
        private string _password;
        private User currentUser;
        private Vendor currentVendor;

        public SignInViewModel(IMvxMessenger messenger, IUserDataService userDataService, IVendorDataService vendorDataService, ILoginDataService loginDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
            _userDataService = userDataService;
            _loginDataService = loginDataService;
        }


        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }


        public MvxCommand SignInCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    currentUser = await _userDataService.SearchUserByEmail(Email);
                    currentVendor = await _vendorDataService.SearchVendorByEmail(Email);

                    //Currently have no contraints for multiple accounts having the same username

                    if (currentUser != null && currentVendor == null)
                    {
                        if (_loginDataService.AuthenticateUser(currentUser, Email, Password))
                        {
                            ShowViewModel<MapViewModel>();
                        }
                    }
                    else if (currentVendor != null && currentUser == null)
                    {
                        if (_loginDataService.AuthenticateVendor(currentVendor, Email, Password))
                        {
                            ShowViewModel<MapViewModel>();
                        }
                    }
                    else
                    {
                        ShowViewModel<LoginViewModel>();
                        //TODO if sign in fails, user goes to login page, should give a warning instead
                    }

                });
            }
        }

    }
}
