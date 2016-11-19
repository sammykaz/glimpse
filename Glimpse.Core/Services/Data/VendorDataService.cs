using System.Collections.Generic;
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
        private Vendor _activeVendor;
        public VendorDataService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<List<Vendor>> SearchUser(string userName)
        {
            return await _vendorRepository.SearchVendor(userName);
        }


        public Vendor GetActiveVendor()
        {
            return _activeVendor;
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