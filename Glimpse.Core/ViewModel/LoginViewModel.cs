using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Model.App;
using Glimpse.Core.Utility;

namespace Glimpse.Core.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(IMvxMessenger messenger) : base(messenger)
        {

        }
        public MvxCommand ShowVendorSignUp
        {
            get { return ShowCommand<VendorSignUpViewModel>(); }
        }

        public MvxCommand ShowStartingMap
        {
            get { return ShowCommand<StartingMapViewModel>(); }
        }

        public MvxCommand ShowUserSignUp
        {
            get { return ShowCommand<UserSignUpViewModel>(); }
        }

        public MvxCommand ShowSignIn
        {
            get { return ShowCommand<SignInViewModel>(); }
        }
    }
}