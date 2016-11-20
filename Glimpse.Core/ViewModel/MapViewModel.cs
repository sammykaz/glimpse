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
        private Location _userCurrentLocation;
        private ObservableCollection<Store> _stores;
        private IStoreDataService _storeDataService;
        private IGeolocator locator;

        
        public MapViewModel(IMvxMessenger messenger,  IStoreDataService storeDataService) : base(messenger)
        {
            _storeDataService = storeDataService;
        }


        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }


        protected override async Task InitializeAsync()
        {            
            //Creates the locator
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 5;          
            
            //Setting up the event and start listening
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(minTime: 1, minDistance: 10);
        }

        public async Task<Location> GetUserLocation()
        {
            //Get the current location            
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            return new Location()
            {
                Lat = position.Latitude,
                Lng = position.Longitude
            };            
        }


        /// <summary>
        /// Event for when position changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            UserCurrentLocation = new Location()
            {
                Lat =  e.Position.Latitude,
                 Lng =  e.Position.Longitude
             };           
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


        public Location UserCurrentLocation
        {
            get { return _userCurrentLocation; }
            set { _userCurrentLocation = value; RaisePropertyChanged(() => UserCurrentLocation); }
        }

    }
    }

