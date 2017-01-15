using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Glimpse.Droid.Extensions;
using Glimpse.Droid.Activities;
using Glimpse.Core.ViewModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MvvmCross.Binding.BindingContext;
using Glimpse.Droid.Helpers;
using MvvmCross.Binding.Droid.BindingContext;
using Android.App;
using Android.Content;
using Android.Locations;
using System.Collections.Generic;
using Com.Google.Maps.Android.Clustering;
using Android.Widget;
using System.Collections;
using Glimpse.Core.Helpers;
using System;
using Exception = System.Exception;
using Android.Graphics;
using Glimpse.Core.Model;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.MapFragment")]
    public class MapFragment : MvxFragment<MapViewModel>, IOnMapReadyCallback, ClusterManager.IOnClusterItemClickListener, ClusterManager.IOnClusterClickListener, RadioGroup.IOnCheckedChangeListener
    {
        private MapView _mapView;
        private GoogleMap map;
        private Marker currentUserLocation;
        private Context globalContext = null;
        private LatLng location = null;
        private ClusterManager clusterManager;
        private List<PromotionItem> clusterList;
        private List<PromotionWithLocation> activePromotions;
        private RadioGroup _radioGroup;
        private int _previousCheckedFilterId;

        public MapFragment()
        {
            clusterList = new List<PromotionItem>();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.MapView, null);
            _mapView = view.FindViewById<MapView>(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);

            return view;
        }


        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);
            // (this.Activity as MainActivity).SetCustomTitle("MapView");
            MapsInitializer.Initialize(Activity);
        }


        public override void OnStart()
        {
            base.OnStart();
        }


        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _mapView.OnDestroy();
        }

        /*   public override void OnSaveInstanceState(Bundle outState)
           {
               base.OnSaveInstanceState(outState);
               _mapView.OnSaveInstanceState(outState);
           } */

        public async override void OnResume()
        {
            base.OnResume();

            globalContext = this.Context;
            //if location services are not enabled do not go further
            if (!CheckLocationServices())
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(globalContext);
                alert.SetTitle("Location services are turned off");
                alert.SetMessage("Please enable Location Services!");
                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                });
                AlertDialog ad = alert.Create();

                ad.Show();

            }
            //location services are on so we can continue
            else
            {
                //Create a progress dialog for loading
                ProgressDialog pr = new ProgressDialog(globalContext);
                pr.SetMessage("Loading Current Position");
                pr.SetCancelable(false);

                var viewModel = (MapViewModel)ViewModel;
                pr.Show();
                //Get the location
                Core.Model.Location locationAsModel = await viewModel.GetUserLocation();

                location = new LatLng(locationAsModel.Lat, locationAsModel.Lng);
                pr.Hide();
                SetUpMap();
            }

            _mapView.OnResume();
        }

        public override void OnPause()

        {
            base.OnPause();
            _mapView.OnPause();
        }

        public override void OnLowMemory()

        {
            base.OnLowMemory();
            _mapView.OnLowMemory();
        }

        private void SetUpMap()
        {
            //Calls the OnMapReady method.
            View.FindViewById<MapView>(Resource.Id.map).GetMapAsync(this);
        }

        private bool CheckLocationServices()
        {
            LocationManager locMgr = (LocationManager)(this.Activity as MainActivity).GetSystemService(Context.LocationService);


            bool gps_enabled = false;
            bool network_enabled = false;

            gps_enabled = locMgr.IsProviderEnabled(LocationManager.GpsProvider);

            network_enabled = locMgr.IsProviderEnabled(LocationManager.NetworkProvider);

            return gps_enabled || network_enabled;
        }



        private void ViewModel_LocationUpdate(object sender, Core.Helpers.LocationChangedHandlerArgs e)
        {
            LatLng latLng = new LatLng(e.Location.Lat, e.Location.Lng);
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLng(latLng);
            map.AnimateCamera(cameraUpdate);
        }


        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            try
            {
                bool success = googleMap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(this.Context, Resource.Raw.style_json));
            }
            catch (System.Exception e)
            {

            }

            var viewModel = (MapViewModel)ViewModel;


            //current user marker
            var options = new MarkerOptions();
            options.SetPosition(location);
            options.SetTitle("My Position");
            options.SetAlpha(0.7f);
            options.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
            options.InfoWindowAnchor(0.7f, 0.7f);
            options.SetSnippet("This is where HARAMBE is hiding!");

            currentUserLocation = map.AddMarker(options);

            //camera initialized on the user            
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(viewModel.DefaulZoom);
            builder.Bearing(viewModel.DefaultBearing);
            builder.Tilt(viewModel.DefaultTilt);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.MoveCamera(cameraUpdate);

            var set = this.CreateBindingSet<MapFragment, MapViewModel>();
            set.Bind(currentUserLocation)
                .For(m => m.Position)
                .To(vm => vm.UserCurrentLocation)
                .WithConversion(new LatLngValueConverter(), null).TwoWay();
            set.Apply();
            ViewModel.LocationUpdate += ViewModel_LocationUpdate;

            //map settings
            map.UiSettings.MapToolbarEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.CompassEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.RotateGesturesEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;
            map.BuildingsEnabled = true;

            //TEST
            clusterManager = new ClusterManager(this.Context, map);
            clusterManager.SetOnClusterClickListener(this);
            clusterManager.SetOnClusterItemClickListener(this);
            map.SetOnCameraIdleListener(clusterManager);
            map.SetOnMarkerClickListener(clusterManager);

            //Show promotions
            ShowPromotionsOnMap();

            //setup radiogroup
            _radioGroup = (RadioGroup)View.FindViewById(Resource.Id.mapfilter_radiogroup);
            _radioGroup.SetOnCheckedChangeListener(this);
        }

        public bool OnClusterClick(ICluster cluster)
        {
            Toast.MakeText(this.Context, "Cluster clicked", ToastLength.Short).Show();
            return false;
        }

        public bool OnClusterItemClick(Java.Lang.Object item)
        {
            PromotionItem promotionItem = (PromotionItem)item;
            StoreItemClick(promotionItem.PromotionId);
            var promotionDialog = new PromotionDialogFragment(promotionItem);
            promotionDialog.Show(this.Activity.FragmentManager, "put a tag here");
            return false;
        }

        private async void StoreItemClick(int promotionId)
        {
            await ViewModel.StorePromotionClick(promotionId);
        }

        private void CreateClusterItem(double lat, double lng, string title, string description, int expirationDate, string companyName, Bitmap image, int promotionId)
        {
            clusterList.Add(new PromotionItem(lat, lng, title, description, expirationDate, companyName, image, promotionId));
        }

        private void GenerateCluster()
        {
            clusterManager.AddItems(clusterList);
        }


        private async void ShowPromotionsOnMap()
        {
            var viewModel = (MapViewModel)ViewModel;

            if(activePromotions ==  null)
            activePromotions = await ViewModel.GetActivePromotions();

            //Print out the pins
            foreach (var p in activePromotions)
            {
                /*
                string companyName = promotion.GetType().GetProperty("CompanyName").GetValue(promotion, null).ToString();
                string title = promotion.GetType().GetProperty("Title").GetValue(promotion, null).ToString();
                string description = promotion.GetType().GetProperty("Description").GetValue(promotion, null).ToString();
                string expirationDate = promotion.GetType().GetProperty("PromotionEndDate").GetValue(promotion, null).ToString();
                byte[] imageBytes = (byte[]) promotion.GetType().GetProperty("PromotionImage").GetValue(promotion, null);

                double lat = (double) PropValue.GetPropertyValue(promotion, "Location.Lat");
                double lng = (double) PropValue.GetPropertyValue(promotion, "Location.Lng");
                */


                //Convert byte array back to image
                Bitmap bitmap = null;
                try
                {
                    bitmap = BitmapFactory.DecodeByteArray(p.Image, 0, p.Image.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error converting byte[] to image" + e.StackTrace);
                }




                CreateClusterItem(p.Location.Lat, p.Location.Lng, p.Title, p.Description, p.Duration, p.CompanyName, bitmap, p.PromotionId);

            }
            GenerateCluster();

        }


        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            if (checkedId == 1)
            {
                ViewModel.SelectedItem = null;
            }
            else
            {
                int checkedId0BasedIndex = checkedId - 2;
                Categories category = (Categories)checkedId0BasedIndex;
                ViewModel.SelectedItem = category;
            }
            
            clusterManager.ClearItems();
            clusterList.Clear();
            ShowPromotionsOnMaBySelectedCategory();
        }

        private  void ShowPromotionsOnMaBySelectedCategory()
        {
            var viewModel = (MapViewModel)ViewModel;
            activePromotions = viewModel.FilteredPromotionList;

            //Print out the pins
            foreach (var p in activePromotions)
            {            
                //Convert byte array back to image
                Bitmap bitmap = null;
                try
                {
                    bitmap = BitmapFactory.DecodeByteArray(p.Image, 0, p.Image.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error converting byte[] to image" + e.StackTrace);
                }

                CreateClusterItem(p.Location.Lat, p.Location.Lng, p.Title, p.Description, p.Duration, p.CompanyName, bitmap, p.PromotionId);
            }
            GenerateCluster();
        }


    }
}