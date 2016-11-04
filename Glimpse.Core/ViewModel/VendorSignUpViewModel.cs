using System.Collections.Generic;
using System.Diagnostics;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using Glimpse.Core.Services.Data;
using System;
using Glimpse.Core.Contracts.Services;

namespace Glimpse.Core.ViewModel
{
    public class VendorSignUpViewModel : BaseViewModel
    {
        private List<User> usersListFromDb;

        private readonly IUserDataService _userDataService;

        public VendorSignUpViewModel(IMvxMessenger messenger, IUserDataService userDataService) : base(messenger)
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


        private string _company;
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                RaisePropertyChanged(() => Company);

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
                        Email = _email,
                        Password = _password
                    };

                    await _userDataService.SignUp(user);                    
                });
            }
        }


    }
}