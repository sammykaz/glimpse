using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;

namespace Glimpse.Core.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(IMvxMessenger messenger) : base(messenger)
        {

        }
        public IMvxCommand ShowVendorSignUp { get { return ShowCommand<VendorSignUpViewModel>(); } }
        public IMvxCommand ShowMapView { get { return ShowCommand<MapViewModel>(); } }

        private MvxCommand ShowCommand<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            return new MvxCommand(() => ShowViewModel<TViewModel>());
        }
    }
}

    

