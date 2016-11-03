using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;

namespace MyTrains.Core.ViewModel
{
    public class MapViewModel: MvxViewModel
    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private Store _store;

        public int DefaulZoom
        {
            get { return _defaultZoom; }
        }


        public int DefaultTilt
        {
            get { return _defaultTilt; }
        }
   
        public int DefaultBearing
        {
            get { return _defaultBearing; }
        }

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

