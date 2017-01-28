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
        private CardViewModel _tilesViewModel;
        private MapViewModel _mapViewModel;

        public ViewPagerViewModel(IMvxMessenger messenger) : base(messenger)
        {
            _mapViewModel = (MapViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<MapViewModel>.GetDefaultRequest(), null);
            _tilesViewModel = (CardViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<CardViewModel>.GetDefaultRequest(), null);
        }

        public MapViewModel MapViewModel
        {
            get
            { 
                if(_mapViewModel ==  null)
                    _mapViewModel = (MapViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<MapViewModel>.GetDefaultRequest(), null);

                return _mapViewModel;
            }
        }

        public CardViewModel TilesViewModel
        {
            get
            {
                if(_tilesViewModel == null)
                    _tilesViewModel = _tilesViewModel = (CardViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(MvxViewModelRequest<CardViewModel>.GetDefaultRequest(), null);

                return _tilesViewModel;

            }
        }
      

    }
}