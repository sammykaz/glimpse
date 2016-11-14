using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.Data;
using Glimpse.Core.ViewModel;


namespace Glimpse.Core
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
       public static bool IsUserLoggedIn { get; set; }

        
        public void Start(object hint = null)
        {
            IsUserLoggedIn = false;
            if (!IsUserLoggedIn)
            {
                ShowViewModel<LoginMainViewModel>();
            }
            else
            {
                ShowViewModel<MainViewModel>();
            }
            

            //var userService = Mvx.Resolve<UserDataService>();
            // var vendorService = Mvx.Resolve<VendorDataService>();

            // var activeUser = userService.GetActiveUser();
            // var activeVendor = vendorService.GetActiveVendor();


 










            
        }
    }
}
