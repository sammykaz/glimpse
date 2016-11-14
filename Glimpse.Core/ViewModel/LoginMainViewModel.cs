using System;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Glimpse.Core.ViewModel
{
    public class LoginMainViewModel : MvxViewModel
    {
        private readonly Lazy<VendorSignUpViewModel> _signupVendorViewModel;
        private readonly Lazy<UserSignUpViewModel> _signupUserViewModel;
        private readonly Lazy<MapViewModel> _mapViewModel;
        private readonly Lazy<SignInViewModel> _signInViewModel;

        public VendorSignUpViewModel VendorSignUpViewModel => _signupVendorViewModel.Value;

        public UserSignUpViewModel UserSignUpViewModel => _signupUserViewModel.Value;
        public MapViewModel MapViewModel => _mapViewModel.Value;
        public SignInViewModel SignInViewModel => _signInViewModel.Value;

        public LoginMainViewModel(IMvxMessenger messenger)
        {
            _mapViewModel = new Lazy<MapViewModel>(Mvx.IocConstruct<MapViewModel>);
            _signupVendorViewModel = new Lazy<VendorSignUpViewModel>(Mvx.IocConstruct<VendorSignUpViewModel>);
            _signupUserViewModel = new Lazy<UserSignUpViewModel>(Mvx.IocConstruct<UserSignUpViewModel>);
            _signInViewModel = new Lazy<SignInViewModel>(Mvx.IocConstruct<SignInViewModel>);
        }

        /*
        public void ShowUserSignUp()
        {
            ShowViewModel<UserSignUpViewModel>();
        }
        public void ShowMapView()
        {
            ShowViewModel<MapViewModel>();
        }
        public void ShowVendorSignUp()
        {
            ShowViewModel<VendorSignUpViewModel>();
        }
        */

        public void ShowLoginPage()
        {
            ShowViewModel<LoginViewModel>();
        }

    }
}

    

