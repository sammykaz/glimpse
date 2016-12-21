using System.Collections.Generic;
using System.Diagnostics;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using Glimpse.Core.Services.Data;
using System;
using System.Linq;
using Amazon.DynamoDBv2;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.ViewModel
{
    public class UserSignUpViewModel : BaseViewModel
    {
        private User user;

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

                    //To be fixed Later

                    //user = await _userDataService.SearchUserByEmail(_email);

                    //Check if email exists in db
                    if (user != null)
                    {
                        //TODO Display Error message to user, choose another email
                    }
                    else
                    {
                        User newUser = new User
                        {
                            FirstName = _firstName,
                            LastName = _lastName,
                            Email = _email,
                            Password = _password,
                            IsVendor = false
                        };

                        //Set as User Account
                        Settings.LoginStatus = true;
                        Settings.Email = _email;
                        

                        await _userDataService.SignUp(newUser);

                        ShowViewModel<MapViewModel>();
                    }
                });
            }
        }
    }
}