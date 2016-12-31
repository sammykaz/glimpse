using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Model;
using Glimpse.Core.Contracts.Services;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Core.Helpers;
using System.Collections;
using System;

namespace Glimpse.Core.ViewModel
{
    public class MapViewModel : BaseViewModel

    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private Dictionary<Vendor, List<Promotion>> _vendorData = new Dictionary<Vendor, List<Promotion>>();
        private IVendorDataService vendorDataService;
        private IPromotionDataService promotionDataService;
        private Location _userCurrentLocation;
        private IGeolocator locator;
        public delegate void LocationChangedHandler(object sender, LocationChangedHandlerArgs e);
        public event LocationChangedHandler LocationUpdate;

        public MapViewModel(IMvxMessenger messenger, IVendorDataService vendorDataService, IPromotionDataService promotionDataService) : base(messenger)
        {
            this.vendorDataService = vendorDataService;
            this.promotionDataService = promotionDataService;
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
                Lat = e.Position.Latitude,
                Lng = e.Position.Longitude
            };
            OnLocationUpdate(UserCurrentLocation);
        }

         private void OnLocationUpdate(Location location)
        {
            if (LocationUpdate != null)
            {
                LocationChangedHandlerArgs args = new LocationChangedHandlerArgs(location);
                LocationUpdate.Invoke(this, args);
            }
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


        public Location UserCurrentLocation
        {
            get { return _userCurrentLocation; }
            set { _userCurrentLocation = value; RaisePropertyChanged(() => UserCurrentLocation); }
        }

        public Dictionary<Vendor, List<Promotion>> VendorData
        {
            get { return _vendorData; }
            set
            {
                _vendorData = value;
                RaisePropertyChanged(() => VendorData);
            }
        }

        public  Task<IEnumerable> GetActivePromotions()
        {
            return promotionDataService.GetActivePromotions();
        }



    }

}

