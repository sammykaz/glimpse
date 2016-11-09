﻿using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.ViewModel;


namespace Glimpse.Core.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly Lazy<VendorSignUpViewModel> _signupVendorViewModel;
        private readonly Lazy<UserSignUpViewModel> _signupUserViewModel;
        private readonly Lazy<MapViewModel> _mapViewModel;      
        private readonly Lazy<LoginPageViewModel> _loginPageViewModel;      

        public MainViewModel()
        {
            _mapViewModel = new Lazy<MapViewModel>(Mvx.IocConstruct<MapViewModel>);
            _loginPageViewModel = new Lazy<LoginPageViewModel>(Mvx.IocConstruct<LoginPageViewModel>);
           _signupVendorViewModel = new Lazy<VendorSignUpViewModel>(Mvx.IocConstruct<VendorSignUpViewModel>);
           _signupUserViewModel = new Lazy<UserSignUpViewModel>(Mvx.IocConstruct<UserSignUpViewModel>);
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
        public void ShowMap()
        {
            ShowViewModel<MapViewModel>();
        }

        public void ShowLoginPage()
        {
            ShowViewModel<LoginPageViewModel>();
        }

        public void ShowVendorSignUp()
        {
            ShowViewModel<VendorSignUpViewModel>();
        }

        public void ShowUserSignUp()
        {
            ShowViewModel<UserSignUpViewModel>();
        }



    }
}