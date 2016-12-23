
using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Plugin.RestClient;
using Glimpse.Core.Services.General;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Glimpse.Core.Services.Data
{
    public class LoginDataService : ILoginDataService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IUserRepository _userRepository;
        private User user;
        private Vendor vendor;

        public LoginDataService(IUserRepository userRepository, IVendorRepository vendorRepository)
        {
            _userRepository = userRepository;
            _vendorRepository = vendorRepository;
        }

        public bool AuthenticateVendor(Vendor vendor, string email, string password)
        {
            string encryptedPassword = Cryptography.EncryptAes(password, vendor.Salt);

            if (email == vendor.Email && encryptedPassword == vendor.Password)
            {
                SaveEmailPasswordInSettings(email, encryptedPassword);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateUser(User user, string email, string password)
        {
            string encryptedPassword = Cryptography.EncryptAes(password, user.Salt);

            if (email == user.Email && encryptedPassword == user.Password)
            {
                SaveEmailPasswordInSettings(email, encryptedPassword);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AuthenticateUserLogin()
        {
           

                bool isValid = false;

                //Check if user already signed in before
                if (!string.IsNullOrEmpty(Settings.Email))
                {
                try
                {
                    //TODO improve this
                    user = await _userRepository.SearchUserByEmail(Settings.Email);
                    vendor = await _vendorRepository.SearchVendorByEmail(Settings.Email);
                }
                catch (Exception e)
                {
                    string lol;
                }

                if (user != null && vendor == null)
                    {
                        if (Settings.Email == user.Email && Settings.Password == user.Password)
                        {
                            isValid = true;
                        }

                    }
                    else if (user == null && vendor != null)
                    {
                        if (Settings.Email == vendor.Email && Settings.Password == vendor.Password)
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

        public void SaveEmailPasswordInSettings(string email, string hashedPassword)
        {
            Settings.Email = email;
            Settings.Password = hashedPassword;
            Settings.LoginStatus = true;
        }

        public void ClearCredentials()
        {
            Settings.Email = string.Empty;
            Settings.Password = string.Empty;
        }

        public void ClearLoginState()
        {
            Settings.LoginStatus = false;
        }

    }
}