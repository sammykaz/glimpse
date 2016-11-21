using Glimpse.Core.ViewModel;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Glimpse.Core.Services.General
{
    // <summary>
    // This is the Settings static class that can be used in your Core solution or in any
    // of your client applications. All settings are laid out the same exact way with getters
    // and setters. 
    // </summary>
    public static class Settings
    {
        private const string UserNameKey = "username_key";
        private static readonly string userName = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string password = string.Empty;

        private const string LoggedInKey = "login_key";
        private static readonly bool isLoggedIn = false;

        private const string IsVendorAccountKey = "is_vendor_account_key";
        private static readonly bool isVendorAccount = false;

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, userName); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value); }
        }

        public static string Password
        {
            get { return AppSettings.GetValueOrDefault<string>(PasswordKey, password); }
            set { AppSettings.AddOrUpdateValue<string>(PasswordKey, value); }
        }
        public static bool LoginStatus
        {
            get { return AppSettings.GetValueOrDefault<bool>(LoggedInKey, isLoggedIn); }
            set { AppSettings.AddOrUpdateValue<bool>(LoggedInKey, value); }
        }

        public static bool IsVendorAccount
        {
            get { return AppSettings.GetValueOrDefault<bool>(IsVendorAccountKey, isVendorAccount); }
            set { AppSettings.AddOrUpdateValue<bool>(IsVendorAccountKey, value); }
        }
    }
}
