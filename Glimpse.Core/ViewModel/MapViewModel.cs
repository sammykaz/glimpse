using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Model;
using Glimpse.Core.Contracts.Services;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.Generic;
using System.Linq;


namespace Glimpse.Core.ViewModel
{
    public class MapViewModel : BaseViewModel

    {
        private readonly int _defaultZoom = 18;
        private readonly int _defaultTilt = 65;
        private readonly int _defaultBearing = 155;
        private List<Vendor> _allVendors;
        private List<Promotion> _allPromotions = new List<Promotion>();
        private Dictionary<Vendor, List<Promotion>> _vendorData = new Dictionary<Vendor, List<Promotion>>();
        private IVendorDataService _vendorDataService;
        private IPromotionDataService _promotionDataService;
        private Location _userCurrentLocation;
        private IGeolocator locator;


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
            get { return _allVendors; }
            set
            {
                _allVendors = value;
                RaisePropertyChanged(() => Vendors);
            }

        }
        public List<Promotion> Promotions
        {
            get { return _allPromotions; }
            set
            {
                _allPromotions = value;
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

        public async Task InitializeData()
        {
            //Get vendors & promotions from dB
            _allVendors = await _vendorDataService.GetVendors();
            _allPromotions = await _promotionDataService.GetPromotions();

            //Get vendor's ids from dB
            foreach (var vendor in _allVendors)
            {
                vendor.dbID = await _vendorDataService.GetVendorId(vendor.UserName);
            }

            //Match active promotions with their vendors
            foreach (var vendor in _allVendors)
            {
                var vendorPromotions = _allPromotions.Where(x => x.VendorId == vendor.dbID && x.PromotionActive == true);

                if (vendorPromotions.Any())
                {
                    VendorData.Add(vendor, vendorPromotions.ToList());
                }
            }
        }
    }
}

