namespace Glimpse.Core.Model
{
    public class Vendor
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string Salt { get; set; }

        public Address Address { get; set; }

        public Telephone Telephone { get; set; }
    }
}