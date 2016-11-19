using System.Collections.Generic;
using System.Threading.Tasks;
using Glimpse.Core.Model;

namespace Glimpse.Core.Contracts.Services
{
    public interface ILoginDataService
    {
        bool AuthenticateVendor(Vendor vendor, string username, string password);
        bool AuthenticateUser(User user, string username, string password);
        bool AuthenticateUserLogin();
        void ClearCredentials();
        void ClearLoginState();
    }
}