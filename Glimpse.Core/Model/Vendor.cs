using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    public class Vendor : User
    {
        private string _company;

        public Vendor(string firstName, string password, string email,  string company) : base(firstName, password, email)
        {
            _company = company;
        }                

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }       

    }
}
