
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Plugin.RestClient;
using Glimpse.Core.Services.General;
using System.Collections.Generic;
using System.Linq;


namespace Glimpse.Core.Services.Data
{
    public class LoginDataService : ILoginDataService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IUserRepository _userRepository;

        public LoginDataService(IUserRepository userRepository, IVendorRepository vendorRepository)
        {
            _userRepository = userRepository;
            _vendorRepository = vendorRepository;
        }

        public bool AuthenticateVendor(Vendor vendor, string username, string password)
        {
            string encryptedPassword = Cryptography.EncryptAes(password, vendor.Salt);

            if (username == vendor.UserName && encryptedPassword == vendor.Password)
            {
                SaveUserNamePasswordInSettings(username, encryptedPassword);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateUser(User user, string username, string password)
        {
            string encryptedPassword = Cryptography.EncryptAes(password, user.Salt);

            if (username == user.UserName && encryptedPassword == user.Password)
            {
                SaveUserNamePasswordInSettings(username, encryptedPassword);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateUserLogin()
        {
            bool isValid = false;

            //Check if user already signed in before
            if (!string.IsNullOrEmpty(Settings.UserName))
            {
                var userList = _userRepository.SearchUser(Settings.UserName).Result;
                var vendorList = _vendorRepository.SearchVendor(Settings.UserName).Result;

                //Currently have no contraints for multiple accounts having the same username

                User user = userList.FirstOrDefault(e => e.UserName == Settings.UserName);
                Vendor vendor = vendorList.FirstOrDefault(e => e.UserName == Settings.UserName);

                if (user != null && vendor == null)
                {
                    if (Settings.UserName == user.UserName && Settings.Password == user.Password)
                    {
                        isValid = true;
                    }

                } else if (user == null && vendor != null)
                {
                    if (Settings.UserName == vendor.UserName && Settings.Password == vendor.Password)
                    {
                        isValid = true;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        private static void SaveUserNamePasswordInSettings(string username, string hashedPassword)
        {
            Settings.UserName = username;
            Settings.Password = hashedPassword;
            Settings.LoginStatus = true;
        }

        public void ClearCredentials()
        {
            Settings.UserName = string.Empty;
            Settings.Password = string.Empty;
        }

        public void ClearLoginState()
        {
            Settings.LoginStatus = false;
        }

    }
}