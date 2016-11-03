using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.ViewModel;
using MyTrains.Core.ViewModel;
using Glimpse.Core.ViewModel;


namespace Glimpse.Core.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly Lazy<SearchJourneyViewModel> _searchJourneyViewModel;
        private readonly Lazy<SavedJourneysViewModel> _savedJourneysViewModel;
        private readonly Lazy<VendorSignUpViewModel> _signupVendorViewModel;
        private readonly Lazy<MapViewModel> _mapViewModel;
        private readonly Lazy<SettingsViewModel> _settingsViewModel;
        private readonly Lazy<LoginPageViewModel> _loginPageViewModel;

        public SearchJourneyViewModel SearchJourneyViewModel => _searchJourneyViewModel.Value;

        public SavedJourneysViewModel SavedJourneysViewModel => _savedJourneysViewModel.Value;

        public SettingsViewModel SettingsViewModel => _settingsViewModel.Value;

        public MainViewModel()
        {
<<<<<<< HEAD
            _mapViewModel = new Lazy<MapViewModel>(Mvx.IocConstruct<MapViewModel>);
            _searchJourneyViewModel = new Lazy<SearchJourneyViewModel>(Mvx.IocConstruct<SearchJourneyViewModel>);
           _savedJourneysViewModel = new Lazy<SavedJourneysViewModel>(Mvx.IocConstruct<SavedJourneysViewModel>);
           _settingsViewModel = new Lazy<SettingsViewModel>(Mvx.IocConstruct<SettingsViewModel>);
=======
            _searchJourneyViewModel = new Lazy<SearchJourneyViewModel>(Mvx.IocConstruct<SearchJourneyViewModel>);
            _savedJourneysViewModel = new Lazy<SavedJourneysViewModel>(Mvx.IocConstruct<SavedJourneysViewModel>);
            _settingsViewModel = new Lazy<SettingsViewModel>(Mvx.IocConstruct<SettingsViewModel>);
            _loginPageViewModel = new Lazy<LoginPageViewModel>(Mvx.IocConstruct<LoginPageViewModel>);
       
>>>>>>> refs/remotes/origin/master
           _signupVendorViewModel = new Lazy<VendorSignUpViewModel>(Mvx.IocConstruct<VendorSignUpViewModel>);
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
        public void ShowMap()
        {
            ShowViewModel<MapViewModel>();
        }
        public void ShowSearchJourneys()
        {
            ShowViewModel<SearchJourneyViewModel>();
        }

        public void ShowLoginPage()
        {
            ShowViewModel<LoginPageViewModel>();
        }

        public void ShowVendorSignUp()
        {
            ShowViewModel<VendorSignUpViewModel>();
        }

   

    }
}