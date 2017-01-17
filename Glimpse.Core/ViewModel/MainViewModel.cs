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

        private Lazy<ViewPagerViewModel> viewPager;
        private readonly Lazy<MapViewModel> _mapViewModel;

        public MapViewModel MapViewModel => _mapViewModel.Value;

        public MainViewModel()
        {
            viewPager = new Lazy<ViewPagerViewModel>(Mvx.IocConstruct<ViewPagerViewModel>);
            _mapViewModel = new Lazy<MapViewModel>(Mvx.IocConstruct<MapViewModel>);
        }




        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
        public void ShowViewPager()

        {
            ShowViewModel<ViewPagerViewModel>();
        }

        public void ShowImageSlider()

        {
            ShowViewModel<TileDetailsViewModel>();
        }

    }
}