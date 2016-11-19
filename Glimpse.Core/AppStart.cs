
using Glimpse.Core.Contracts.Services;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.ViewModel;
using MvvmCross.Platform;


namespace Glimpse.Core
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            //Check if the user is logged in before and authenticate
            var authenticator = Mvx.Resolve<ILoginDataService>();

            if (authenticator.AuthenticateUserLogin())
            {
                ShowViewModel<MainViewModel>();
            }
            else
            {
                ShowViewModel<LoginMainViewModel>();
            }
        }
    }
}
