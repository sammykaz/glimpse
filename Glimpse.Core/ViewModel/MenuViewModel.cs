using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
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
        private IUserDataService _userDataService;
        private IVendorDataService _vendorDataService;
        private User _user;
        private Vendor _vendor;

        public MenuViewModel(IMvxMessenger messenger, ILoginDataService loginDataService, IUserDataService userDataService, IVendorDataService vendorDataService) : base(messenger)
        {
            MenuItems = new ObservableCollection<MenuItem>();
            _loginDataService = loginDataService;
            _userDataService = userDataService;
            _vendorDataService = vendorDataService;
            CreateMenuItems();
        }

        private async void CreateMenuItems()
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

            MenuItems.Add(new MenuItem
            {
                Title = "Map",
                ViewModelType = typeof(ViewPagerViewModel),
                Option = MenuOption.Logout,
                IsSelected = false
            });

            _user = await _userDataService.SearchUserByEmail(Settings.Email);
            _vendor = await _vendorDataService.SearchVendorByEmail(Settings.Email);

            if (_user != null && _vendor == null)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Buyer Profile",
                    ViewModelType = typeof(BuyerProfilePageViewModel),
                    Option = MenuOption.BuyerProfile,
                    IsSelected = true
                });
            }
            else if (_user == null && _vendor != null)
            {
                MenuItems.Add(new MenuItem
                {
                    Title = "Vendor Profile",
                    ViewModelType = typeof(VendorProfilePageViewModel),
                    Option = MenuOption.VendorProfile,
                    IsSelected = false
                });
            }
            else
            {
                //do nothing, no profile for no user
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