using System.Threading.Tasks;
using Glimpse.Core.Contracts.Repository;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;

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

        public async Task SignUp(string userName, string password, string email, string company)
        {
            await _vendorRepository.SignUp(userName, password, email, company);
        }
    }
}