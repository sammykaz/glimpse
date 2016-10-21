using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class User
    {

        private string _firstName;
        private string _email;
        private string _password;


        public User(string firstName, string password, string email)
        {
            _firstName = firstName;
            _email = email;
            _password = password;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
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
