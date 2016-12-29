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
        private List<Vendor> allVendors;
        private List<Promotion> allPromotions = new List<Promotion>();
        private Dictionary<Vendor, List<Promotion>> _vendorData = new Dictionary<Vendor, List<Promotion>>();
        private IVendorDataService _vendorDataService;
        private IPromotionDataService _promotionDataService;
        private Location _userCurrentLocation;
        private IGeolocator locator;
        public delegate void LocationChangedHandler(object sender, LocationChangedHandlerArgs e);
        public event LocationChangedHandler LocationUpdate;

        public MapViewModel(IMvxMessenger messenger, IVendorDataService vendorDataService, IPromotionDataService promotionDataService) : base(messenger)
        {
            _vendorDataService = vendorDataService;
            _promotionDataService = promotionDataService;
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

        public List<Vendor> Vendors
        {
            get { return allVendors; }
            set
            {
                allVendors = value;
                RaisePropertyChanged(() => Vendors);
            }

        }
        public List<Promotion> Promotions
        {
            get { return allPromotions; }
            set
            {
                allPromotions = value;
                RaisePropertyChanged(() => Promotions);
            }
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

        public async Task<IEnumerable> GetActivePromotions()
        {
            allPromotions = await _promotionDataService.GetPromotions();
            allVendors = await _vendorDataService.GetVendors();

            List<Promotion> activePromotions = allPromotions.Where(e => (e.PromotionEndDate - e.PromotionStartDate).TotalSeconds > 0).ToList();

            var mapPromotions = allVendors.Join(activePromotions, e => e.VendorId, b => b.VendorId,
                                (e, b) => new
                                {
                                    e.CompanyName,
                                    e.Location,
                                    b.Title,
                                    b.Description,
                                    b.PromotionImage,
                                    b.PromotionEndDate
                                });

            //Select all promotions excluding those with empty locations
            var validatedMapPromotions = mapPromotions.Where(e => e.Location.Lat != 0 || e.Location.Lng != 0);

            return validatedMapPromotions;
        }




    }

}

