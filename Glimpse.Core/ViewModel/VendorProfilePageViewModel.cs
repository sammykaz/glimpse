using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;

namespace Glimpse.Core.ViewModel
{
    public class VendorProfilePageViewModel : BaseViewModel
    {
        public VendorProfilePageViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }


        public IMvxCommand ShowCreatePromotionView { get { return ShowCommand<CreatePromotionViewModel>(); } }
      
        private MvxCommand ShowCommand<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            return new MvxCommand(() => ShowViewModel<TViewModel>());
        }
    }
}
