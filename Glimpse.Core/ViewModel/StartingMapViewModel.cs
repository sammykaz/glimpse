using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Glimpse.Core.Contracts.ViewModel;
using Glimpse.Core.ViewModel;


namespace Glimpse.Core.ViewModel
{
    public class StartingMapViewModel : MvxViewModel, IMainViewModel
    {

        public StartingMapViewModel()
        {
        }


        public void ShowStartingMap()
        {
            ShowViewModel<StartingMapViewModel>();
        }

    }
}