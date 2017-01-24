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
using System.Windows.Input;

namespace Glimpse.Core.ViewModel
{
    public class TilesViewModel : BaseViewModel
    {
        private string _currentLanguage;        
        private IPromotionDataService _promotionDataService;
        private IVendorDataService _vendorDataService;
        private List<PromotionWithLocation> _promotions;

        private List<PromotionWithLocation> _promotionsStored;

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

        public async Task ReloadAsync()
        {
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

            PromotionList = await GetPromotionsWithLocation();

            _promotionsStored = PromotionList;
        }

        private Categories? _selectedItem;
        public Categories? SelectedItem
        {
            get
            {                
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                PromotionList = _promotionDataService.FilterPromotionWithLocationList(_promotionsStored, _selectedItem);
                RaisePropertyChanged(() => PromotionList);
            }
        }


        private List<string> _categories;
        public List<string> Categories
        {
            get
            {
                List<string> allCategories = new List<string>();
                allCategories.Add("All");
                foreach(string name in Enum.GetNames(typeof(Categories)))
                {
                    allCategories.Add(name);
                };
                return allCategories;
            }
            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
            }
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


        public async Task<List<PromotionWithLocation>> GetPromotionsWithLocation()
        {


            var mapPromotions = await _promotionDataService.GetActivePromotions();


            List<Location> promotionLocations = mapPromotions.Select(promotionWithLocation => promotionWithLocation.Location).ToList();

            List<IEnumerable<Location>> splitPromotionLocation = promotionLocations.Chunk(10).ToList();


            //index to plug result into mapPromotions list
            int j = 0;
            foreach (IEnumerable<Location> subList in splitPromotionLocation)
            {
                List<Location> subListAsList = subList.ToList();
                DistanceMatrix distanceMatrix = await _gwb.GetMultipleDurationResponse(_userLocation, subListAsList);

                for (int i = 0; i < subListAsList.Count; i++)
                {
                    if (distanceMatrix.rows[0].elements[i].status.Equals("OK"))
                    {
                        mapPromotions[j].Duration = distanceMatrix.rows[0].elements[i].duration.value;
                    }
                    j++;

                }

            }

            List<PromotionWithLocation> final = mapPromotions.OrderBy(promotion => promotion.Duration).ToList().FindAll(p => p.Duration != 9999);

            final = await _promotionDataService.PopulatePromotionWithLocationBlobs(final);

            return final;
        }

        private bool _isRefreshing;

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public MvxCommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    IsRefreshing = true;

                    await ReloadAsync();

                    IsRefreshing = false;
                });
            }
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

        public ICommand ViewTileDetails
        {
            get
            {
                return new MvxCommand<PromotionWithLocation>(item =>
                {
                    var desc = new Dictionary<string, string> {
                        {"PromotionID", Convert.ToString(item.PromotionId)} };

                    ShowViewModel<TileDetailsViewModel>(desc);

                });
            }
        }
    }

    public static class EnumerableExtensions
    {

        /// <summary>
        /// Break a list of items into chunks of a specific size
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }
    }
}