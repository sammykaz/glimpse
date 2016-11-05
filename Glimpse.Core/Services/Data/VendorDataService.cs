using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Services.General;

namespace Glimpse.Core.Services.Data
{
    public class VendorDataService: IVendorDataService
    {
        private readonly IVendorRepository _vendorRepository;
        private User _activeUser;
        public VendorDataService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<User> SearchUser(string userName)
        {
            return await _vendorRepository.SearchUser(userName);
        }

        public async Task<User> Login(string userName, string password)
        {
            _activeUser = await _vendorRepository.Login(userName, password);
            return _activeUser;
        }

        public User GetActiveUser()
        {
            return _activeUser;
        }

        public async Task SignUp(Vendor vendor)
        {
            var cryptoTuple = Cryptography.EncryptAes(vendor.Password);
            vendor.Password = cryptoTuple.Item1;
            vendor.Salt = cryptoTuple.Item2;
            await _vendorRepository.PostVendor(vendor);
        }
    }
}