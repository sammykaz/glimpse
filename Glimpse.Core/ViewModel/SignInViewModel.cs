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
        private IVendorDataService _vendorDataService;
        private IUserDataService _userDataService;
        private ILoginDataService _loginDataService;
        private string _userName;
        private string _password;

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

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }


        public MvxCommand SignInCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    List<User> userList = await _userDataService.SearchUser(UserName);
                    List<Vendor> vendorList = await _vendorDataService.SearchUser(UserName);

                    //Currently have no contraints for multiple accounts having the same username

                    User user = userList.FirstOrDefault(e => e.UserName == UserName);
                    Vendor vendor = vendorList.FirstOrDefault(e => e.UserName == UserName);


                    if (user != null && vendor == null)
                    {
                        if (_loginDataService.AuthenticateUser(user, UserName, Password))
                        {
                            ShowViewModel<MapViewModel>();
                        }
                    }
                    else if (vendor != null && user == null)
                    {
                        if (_loginDataService.AuthenticateVendor(vendor, UserName, Password))
                        {
                            ShowViewModel<MapViewModel>();
                        }
                    }
                    else
                    {
                        ShowViewModel<LoginViewModel>();
                    }

                });
            }
        }

    }
}
