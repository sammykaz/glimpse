
using Glimpse.Core.Contracts.Services;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.ViewModel;
using MvvmCross.Platform;
using Glimpse.Core.Services.General;

namespace Glimpse.Core
{
    public class AppStart: MvxNavigatingObject, IMvxAppStart
    {
        public async void Start(object hint = null)
        {
            //Check if the user is logged in before and authenticate
            var authenticator = Mvx.Resolve<ILoginDataService>();
            //Setting the default language of the app
            if(Settings.Language == string.Empty)
            Settings.Language = "English";

            
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
