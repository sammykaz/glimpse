using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class ViewPagerViewModel : BaseViewModel
    {
        public Lazy<TilesViewModel> _tilesViewModel;
        public Lazy<MapViewModel> _mapViewModel;

        public ViewPagerViewModel(IMvxMessenger messenger) : base(messenger)
        {
             _tilesViewModel = new Lazy<TilesViewModel>(Mvx.IocConstruct<TilesViewModel>);
             _mapViewModel = new Lazy<MapViewModel>(Mvx.IocConstruct<MapViewModel>);
        }

        public MapViewModel MapViewModel
        {
            get
            {
                return (MapViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<MapViewModel>.GetDefaultRequest(), null);

            }
        }

        public TilesViewModel TilesViewModel
        {
            get
            {
                return (TilesViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<TilesViewModel>.GetDefaultRequest(), null);

            }
        }
      

    }
}