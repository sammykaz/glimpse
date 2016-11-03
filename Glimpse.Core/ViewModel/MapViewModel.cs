using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using System.Collections.ObjectModel;
using Glimpse.Core.Contracts.Services;
using System.Threading.Tasks;
using Glimpse.Core.Extensions;

namespace MyTrains.Core.ViewModel
{
    public class MapViewModel:  MvxViewModel
    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private Store _store;
        private ObservableCollection<Store> _stores;
        private IStoreDataService _storeDataService;

        public MapViewModel(IStoreDataService storeDataService) 
        {
            _storeDataService = storeDataService;
            LoadStores();
        }

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


        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set
            {
                _stores = value;
                RaisePropertyChanged(() => Stores);
            }
        }

        public Store Store
        {
            get { return _store; }
            set { _store = value; RaisePropertyChanged(() => Store); }
        }


        internal async Task LoadStores()
        {
            _stores = (await _storeDataService.GetAllStores()).ToObservableCollection();
            Store = Stores[0];
        }


        




        }
    }

