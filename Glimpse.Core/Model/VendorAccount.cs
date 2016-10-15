using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class VendorAccount
    {

        private string _firstName;
        private string _company;
        private string _email;
        private string _password;


        public VendorAccount(string firstName, string company, string email, string password)
        {
            _firstName = firstName;
            _company = company;
            _email = email;
            _password = password;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}
