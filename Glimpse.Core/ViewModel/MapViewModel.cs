using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;

namespace MyTrains.Core.ViewModel
{
    public class MapViewModel
        : MvxViewModel
    {
        private First _store;
        public First Store
        {
            get { return _store; }
            set { _store = value; RaisePropertyChanged(() => Store); }
        }


        public MapViewModel()
        {
            Store = new First()
            {
                Name = "Store",
                Location = new Location()
                {
                    Lat = 45.5017,
                    Lng = -73.5673
                },
            };
         
                
            }
        }
    }

