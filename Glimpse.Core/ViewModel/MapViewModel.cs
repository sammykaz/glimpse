using MvvmCross.Plugins.Messenger;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using Glimpse.Core.Model;
using System.Collections.ObjectModel;
using Glimpse.Core.Contracts.Services;
using System.Threading.Tasks;
using Glimpse.Core.Extensions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using MvvmCross.Platform.WeakSubscription;

namespace Glimpse.Core.ViewModel
{
    public class MapViewModel:  BaseViewModel

    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private Store _store;
        private ObservableCollection<Store> _stores;
        private IStoreDataService _storeDataService;
        private IGeolocator locator;
        //locator.DesiredAccuracy = 50;



        public MapViewModel(IMvxMessenger messenger,  IStoreDataService storeDataService) : base(messenger)

        {
            _storeDataService = storeDataService;
            _store = new Store()
            {
                Name = "Store",
                Location = new Location()
                {
                    Lat = 45.5017,
                    Lng = -73.5673
               }
           };



        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            
           
            var location = new Location()
            {
                Lat =  e.Position.Latitude,
                 Lng =  e.Position.Longitude
             };

            Store.Location = location;
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            // _stores = (await _storeDataService.GetAllStores()).ToObservableCollection();
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            Store =new Store()
            {
                Name = "Store",
                Location = new Location()
                {
                    Lat = position.Latitude,
                    Lng = position.Longitude
                }
            };

            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(minTime: 1, minDistance: 10);
           
            
     

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
            //_stores = (await _storeDataService.GetAllStores()).ToObservableCollection();
            //Store = Stores[0];

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

           Store= new Store()
            {
                Name = "Store",
                Location = new Location()
                {
                    Lat = position.Latitude,
                    Lng = position.Longitude
                }
            };
         
        }

        

        public MvxCommand MoveCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    var location = new Location()
                    {
                        Lat = Store.Location.Lat + 0.1,
                        Lng = Store.Location.Lng
                    };

                    Store.Location = location;

                    var bp = 1;
                    
                });
            }
        }

       // private MvxNamedNotifyPropertyChangedEventSubscription<> _selectedItemToken;


    }
    }

