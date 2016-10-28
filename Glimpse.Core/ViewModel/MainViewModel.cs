﻿using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.ViewModel;

namespace Glimpse.Core.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly Lazy<SearchJourneyViewModel> _searchJourneyViewModel;
        private readonly Lazy<SavedJourneysViewModel> _savedJourneysViewModel;
        private readonly Lazy<SettingsViewModel> _settingsViewModel;
        private readonly Lazy<LoginPageViewModal> _loginPageViewModel;

        public SearchJourneyViewModel SearchJourneyViewModel => _searchJourneyViewModel.Value;

        public SavedJourneysViewModel SavedJourneysViewModel => _savedJourneysViewModel.Value;

        public SettingsViewModel SettingsViewModel => _settingsViewModel.Value;

        public MainViewModel()
        {
            _searchJourneyViewModel = new Lazy<SearchJourneyViewModel>(Mvx.IocConstruct<SearchJourneyViewModel>);
            _savedJourneysViewModel = new Lazy<SavedJourneysViewModel>(Mvx.IocConstruct<SavedJourneysViewModel>);
            _settingsViewModel = new Lazy<SettingsViewModel>(Mvx.IocConstruct<SettingsViewModel>);
            _loginPageViewModel = new Lazy<LoginPageViewModal>(Mvx.IocConstruct<LoginPageViewModal>);
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }

        public void ShowSearchJourneys()
        {
            ShowViewModel<SearchJourneyViewModel>();
        }

        public void ShowLoginPage()
        {
            ShowViewModel<LoginPageViewModal>();
        }
    }
}