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
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<VendorSignUpViewModel>(new { index = 0 });
                });
            }
        }

        public MvxCommand ShowMap
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<MapViewModel>(new {index = 0});
                });
            }
        }

        public MvxCommand ShowSignIn
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<SignInViewModel>(new { index = 0 });
                });
             }
        }
    }
}