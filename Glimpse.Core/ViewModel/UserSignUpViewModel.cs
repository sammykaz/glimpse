using System.Collections.Generic;
using System.Diagnostics;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using Glimpse.Core.Services.Data;
using System;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.ViewModel
{
    public class UserSignUpViewModel : BaseViewModel
    {
        private List<User> usersListFromDb;

        private readonly IUserDataService _userDataService;

        public UserSignUpViewModel(IMvxMessenger messenger, IUserDataService userDataService) : base(messenger)
        {
            _userDataService = userDataService;
        }


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);

            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);

            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);

            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);

            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public MvxCommand SignUpCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    User user = new User
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Email = _email,
                        UserName = _userName,
                        Password = _password
                    };

                    //Set as User Account
                    Settings.IsVendorAccount = false;

                    await _userDataService.SignUp(user);

                    ShowCommand<LoginViewModel>();

                });
            }
        }
    }
}