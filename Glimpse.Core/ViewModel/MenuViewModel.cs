using System;
using System.Collections.ObjectModel;
using Glimpse.Core.Contracts.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Model.App;
using Glimpse.Core.Services.General;
using Glimpse.Core.Utility;
using MvvmCross.Platform;

namespace Glimpse.Core.ViewModel
{
    public class MenuViewModel: BaseViewModel
    {
        public MvxCommand<MenuItem> MenuItemSelectCommand => new MvxCommand<MenuItem>(OnMenuEntrySelect);
        public ObservableCollection<MenuItem> MenuItems { get; }

        public event EventHandler CloseMenu;

        private ILoginDataService _loginDataService;

        private string selectedMenuOption;



        public MenuViewModel(IMvxMessenger messenger, ILoginDataService loginDataService) : base(messenger)
        {
            MenuItems = new ObservableCollection<MenuItem>();
            CreateMenuItems();
            _loginDataService = loginDataService;
        }

        private void CreateMenuItems()
        {
            MenuItems.Add(new MenuItem
            {
                Title = "Logout",
                ViewModelType = typeof(LoginMainViewModel),
                Option = MenuOption.Logout,
                IsSelected = false
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Settings",
                ViewModelType = typeof(SettingsViewModel),
                Option = MenuOption.Settings,
                IsSelected = false
            });

            if (!Settings.IsVendorAccount)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Buyer Profile",
                    ViewModelType = typeof(BuyerProfilePageViewModel),
                    Option = MenuOption.BuyerProfile,
                    IsSelected = true
                });
            }

            if (Settings.IsVendorAccount)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Vendor Profile",
                    ViewModelType = typeof(VendorProfilePageViewModel),
                    Option = MenuOption.VendorProfile,
                    IsSelected = false
                });
            }
        }

        private void OnMenuEntrySelect(MenuItem item)
        {
            if (item.Option == MenuOption.Logout)
            {
                _loginDataService.ClearCredentials();
                _loginDataService.ClearLoginState();
            }
                ShowViewModel(item.ViewModelType);
            
            RaiseCloseMenu();
        }

        public void SetSelectedMenuOption(string menuOption)
        {
            this.selectedMenuOption = menuOption;
        }

        public string GetSelectedMenuOption()
        {
            return this.selectedMenuOption;
        }

        private void RaiseCloseMenu()
        {
            CloseMenu?.Invoke(this, EventArgs.Empty);
        }
    }
}