using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.ViewModel;
using Glimpse.Core.Services;
using Glimpse.Core.Services.Data;
using Glimpse.Core.Repositories;

namespace Glimpse.Core.ViewModel
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
       // public MapViewModel _mapViewModel;
        public Lazy<TilesViewModel> _tilesViewModel;
         public Lazy<MapViewModel> _mapViewModel;
        /* private VendorRepository _vendorRepository;
         private VendorDataService _vendorDataService;
         private PromotionRepository _promotionRepository;
         private PromotionDataService _promotionDataService;*/

        public MainViewModel()
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


        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
        public void ShowMap()
        {
            ShowViewModel<MapViewModel>();
        }
      


    }
}