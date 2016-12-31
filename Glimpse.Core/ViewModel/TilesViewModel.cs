using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Model;
using Glimpse.Core.Model.CustomModels;
using Glimpse.Core.Services.General;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Glimpse.Core.ViewModel
{
    public class TilesViewModel : BaseViewModel
    {
        private string _currentLanguage;
        private List<string> _languages;
        private IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        private List<PromotionWithLocation> _promotions;

        private IGeolocator locator;
        private Location _userLocation;

        private GoogleWebService _gwb;


        public TilesViewModel(IMvxMessenger messenger, IPromotionDataService promotionDataService, IVendorDataService vendorDataService) : base(messenger)
        {
            _promotionDataService = promotionDataService;
            _vendorDataService = vendorDataService;
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

            //creates the google web service wrapper
            _gwb = new GoogleWebService();

            //get initial user location
            _userLocation = await GetUserLocation();

            PromotionList = await getPromotionsWithLocation();
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


        private async Task<List<PromotionWithLocation>> getPromotionsWithLocation()
        {
            List<Promotion> allPromotions = await _promotionDataService.GetPromotions();
            List<Vendor> allVendors = await _vendorDataService.GetVendors();

            DateTime now = DateTime.Now;            

            List<Promotion> activePromotions = allPromotions.Where(e => e.PromotionStartDate.CompareTo(now) <= 0  && e.PromotionEndDate.CompareTo(now) >= 0).ToList();

            var mapPromotions = allVendors.Join(activePromotions, e => e.VendorId, b => b.VendorId,
                                (e, b) => new PromotionWithLocation
                                {
                                    Title = b.Title,
                                    Location = e.Location,
                                    Description = b.Description,
                                    CompanyName = e.CompanyName,
                                    Duration = 9999,
                                    Image = b.PromotionImage                                             
                                }).ToList();

            List<Location> promotionLocations = mapPromotions.Select(promotionWithLocation => promotionWithLocation.Location).ToList();

            DistanceMatrix distanceMatrix = await _gwb.GetMultipleDurationResponse(_userLocation, promotionLocations);

            
            for(int i = 0; i < mapPromotions.Count; i++)
            {
                if(distanceMatrix.rows[0].elements[i].status.Equals("OK"))
                {
                    mapPromotions[i].Duration = distanceMatrix.rows[0].elements[i].duration.value;
                }
               
            }

            List<PromotionWithLocation> final = mapPromotions.OrderBy(promotion => promotion.Duration).ToList().FindAll(p => p.Duration != 9999);
            //GetMultipleDurationResponse(Location origin, List < Location > destination)

            return final;
        }

        public List<PromotionWithLocation> PromotionList
        {
            get { return _promotions; }
            set
            {
                _promotions = value;
                RaisePropertyChanged(() => PromotionList);
            }

        }
      


    }
}