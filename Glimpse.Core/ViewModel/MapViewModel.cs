using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;

namespace MyTrains.Core.ViewModel
{
    public class MapViewModel
        : MvxViewModel
    {
        private Store _store;
        public Store Store
        {
            get { return _store; }
            set { _store = value; RaisePropertyChanged(() => Store); }
        }


        public MapViewModel()
        {
            Store = new Store()
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

