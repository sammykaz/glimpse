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
    public class VendorSignUpViewModel : BaseViewModel
    {
        private List<Vendor> vendorsListFromDb;

        private readonly IVendorDataService _vendorDataService;
        private readonly IUserDataService _userDataService;

        public VendorSignUpViewModel(IMvxMessenger messenger, IVendorDataService vendorDataService, IUserDataService userDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
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

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                RaisePropertyChanged(() => CompanyName);

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

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                RaisePropertyChanged(() => Country);

            }
        }

        private string _province;
        public string Province
        {
            get { return _province; }
            set
            {
                _province = value;
                RaisePropertyChanged(() => Province);

            }
        }


        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);

            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                RaisePropertyChanged(() => PostalCode);

            }
        }

        private string _street;
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                RaisePropertyChanged(() => Street);

            }
        }

        private string _streetNumber;
        public string StreetNumber
        {
            get { return _streetNumber; }
            set
            {
                _streetNumber = value;
                RaisePropertyChanged(() => StreetNumber);

            }
        }

        private string _personalPhoneNumber;
        public string PersonalPhoneNumber
        {
            get { return _personalPhoneNumber; }
            set
            {
                _personalPhoneNumber = value;
                RaisePropertyChanged(() => PersonalPhoneNumber);

            }
        }

        private string _businessPhoneNumber;
        public string BusinessPhoneNumber
        {
            get { return _businessPhoneNumber; }
            set
            {
                _businessPhoneNumber = value;
                RaisePropertyChanged(() => BusinessPhoneNumber);

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
                        LastName = _lastName,
                        CompanyName = _companyName,
                        Email = _email,
                        UserName = _userName,
                        Password = _password,
                        Address = new Address() {Country = _country, Province = _province, City = _city, PostalCode = _postalCode, Street = _street, StreetNumber = _streetNumber},
                        Telephone = new Telephone() {PersonalPhoneNumber = _personalPhoneNumber, BusinessPhoneNumber = _businessPhoneNumber}
                    };

                    //Set as Vendor Account
                    Settings.IsVendorAccount = true;

                    ShowCommand<LoginViewModel>();

                    await _vendorDataService.SignUp(vendor);

                   
                });
            }
        }


    }
}