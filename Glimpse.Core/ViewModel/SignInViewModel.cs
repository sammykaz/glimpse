using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Glimpse.Core.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private IVendorDataService _vendorDataService;
        private IUserDataService _userDataService;

        private string _userName;
        private string _password;
        
        public SignInViewModel(IMvxMessenger messenger, IUserDataService userDataService, IVendorDataService vendorDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
            _userDataService = userDataService;
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
                return new MvxCommand(() =>
                {
                    //Vendor vendor = _vendorDataService.VerifyCredentials(UserName, Password);
                    //TODO decrypt password here 


                });
            }
        }

    }
}
