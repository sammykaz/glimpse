using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using MyTrains.Core.ViewModel;

namespace Glimpse.Core.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(IMvxMessenger messenger) : base(messenger)
        {

        }
        public IMvxCommand ShowVendorSignUp { get { return ShowCommand<VendorSignUpViewModel>(); } }

        private MvxCommand ShowCommand<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            return new MvxCommand(() => ShowViewModel<TViewModel>());
        }
    }
}

    

