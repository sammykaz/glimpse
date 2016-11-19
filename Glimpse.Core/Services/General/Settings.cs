using Glimpse.Core.ViewModel;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Glimpse.Core.Services.General
{
    public static class Settings
    {
        private const string UserNameKey = "username_key";
        private static readonly string userName = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string password = string.Empty;

        private const string LoggedInKey = "login_key";
        private static readonly bool isLoggedIn = false;

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
    }
}
