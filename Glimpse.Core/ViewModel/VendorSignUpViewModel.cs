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
        private List<Vendor> vendorsListFromDb;

        private readonly IVendorDataService _vendorDataService;

        public VendorSignUpViewModel(IMvxMessenger messenger, IVendorDataService vendorDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
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
                    Vendor vendor = new Vendor()
                    {
                        FirstName = _firstName,
                        Company = _company,
                        Email = _email,
                        Password = _password
                    };

                    await _vendorDataService.SignUp(vendor);                    
                });
            }
        }


    }
}