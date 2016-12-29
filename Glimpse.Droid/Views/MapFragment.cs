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
using Glimpse.Core.Model;
using Android.Locations;
using Android.Content;
using System;
using System.Collections.Generic;
using Com.Google.Maps.Android.Clustering;
using Java.Lang;
using Android.Widget;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.MapFragment")]
    public class MapFragment : MvxFragment<MapViewModel>, IOnMapReadyCallback, ClusterManager.IOnClusterClickListener, ClusterManager.IOnClusterItemClickListener
    {
        private MapView _mapView;
        private GoogleMap _map;
        private Marker _currentUserLocation;
        private Context globalContext = null;
        private LatLng location = null;
        private Marker _promotion;
        private ClusterManager _clusterManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.MapView, null);
            _mapView = view.FindViewById<MapView>(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);
            _mapView.GetMapAsync(this);

            return view;
        }


        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);
            (this.Activity as MainActivity).SetCustomTitle("MapView");
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
            //_mapView = null;
           // _map = null;
            _currentUserLocation = null;
        }

     /*   public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _mapView.OnSaveInstanceState(outState);
        } */

        public async override void OnResume()
        {
            base.OnResume();
            // SetUpMapIfNeeded();

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
                InitializeMapAndHandlers();
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

        private void SetUpMapIfNeeded()
        {
            if (_map== null)
            {
                View.FindViewById<MapView>(Resource.Id.map).GetMapAsync(this);
            }
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


        private async void InitializeMapAndHandlers()
        {
            SetUpMapIfNeeded();
            var viewModel = (MapViewModel)ViewModel;


            /*
            List<Promotion> activePromotions = await viewModel.GetAllActivePromotions();
            List<Vendor> activeVendors = await viewModel.GetAllVendorsWithActivePromotions();

            if (activePromotions != null && activeVendors != null)
            {
                foreach (var activeVendor in activeVendors)
                {
                    _promotion = _map.AddMarker(
                        new MarkerOptions()
                            .SetPosition(new LatLng(activeVendor.Location.Lat, activeVendor.Location.Lng))
                            .SetTitle(activeVendor.CompanyName)
                            .SetSnippet("Currently has: " + activePromotions.Count + " promotion" +
                                        (activePromotions.Count > 1 ? "s" : "")));
                }
            }
            */

            /*
            foreach(var vendor in viewModel.VendorData.Keys)
            {
                var numberOfPromotions = viewModel.VendorData[vendor].Count;

                _promotion = _map.AddMarker(
                        new MarkerOptions()
                            .SetPosition(new LatLng(vendor.Location.Lat, vendor.Location.Lng))
                            .SetTitle(vendor.CompanyName)
                            .SetSnippet("Currently has: " + numberOfPromotions+ " promotion" + (numberOfPromotions > 1 ? "s" : "")));
            }
            */

            //map settings
            _map.UiSettings.MapToolbarEnabled = true;
            _map.UiSettings.ZoomControlsEnabled = true;
            _map.UiSettings.CompassEnabled = true;
            _map.UiSettings.MyLocationButtonEnabled = true;
            _map.UiSettings.RotateGesturesEnabled = true;
            _map.UiSettings.ZoomGesturesEnabled = true;
            _map.BuildingsEnabled = true;

            //TEST
            SetViewPoint(new LatLng(63.430515, 10.395053), false);

            _clusterManager = new ClusterManager(this.Context, _map);
            _clusterManager.SetOnClusterClickListener(this);
            _clusterManager.SetOnClusterItemClickListener(this);
            _map.SetOnCameraIdleListener(_clusterManager);
            _map.SetOnMarkerClickListener(_clusterManager);

            AddClusterItems();
            //current user marker
            /*var options = new MarkerOptions();
            options.SetPosition(location);
            options.SetTitle("My Position");
            options.SetAlpha(0.7f);
            options.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
            options.InfoWindowAnchor(0.7f, 0.7f);
            options.SetSnippet("This is where HARAMBE is hiding!");

            _currentUserLocation = _map.AddMarker(options);
            */
            //camera initialized on the user            
            /*CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(viewModel.DefaulZoom);
            builder.Bearing(viewModel.DefaultBearing);
            builder.Tilt(viewModel.DefaultTilt);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            _map.MoveCamera(cameraUpdate);

            var set = this.CreateBindingSet<MapFragment, MapViewModel>();
            set.Bind(_currentUserLocation)
                .For(m => m.Position)
                .To(vm => vm.UserCurrentLocation)
                .WithConversion(new LatLngValueConverter(), null).TwoWay();
            set.Apply();
            ViewModel.LocationUpdate += ViewModel_LocationUpdate;*/
        }

        private void ViewModel_LocationUpdate(object sender, Core.Helpers.LocationChangedHandlerArgs e)
        {
            LatLng latLng = new LatLng(e.Location.Lat, e.Location.Lng);
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLng(latLng);
            _map.AnimateCamera(cameraUpdate);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            try
            {

                bool success = googleMap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(this.Context, Resource.Raw.style_json));

            }
            catch (System.Exception e)
            {

            }
        }

        public bool OnClusterClick(ICluster cluster)
        {
            Toast.MakeText(this.Context, cluster.Items.Count + " items in cluster", ToastLength.Short).Show();
            return false;
        }

        public bool OnClusterItemClick(Java.Lang.Object p0)
        {
            	Toast.MakeText (this.Context, "Marker clicked", ToastLength.Short).Show ();
			return false;
        }
        private void AddClusterItems()
        {
            double lat = 63.430515;
            double lng = 10.395053;

            List<ClusterItem> items = new List<ClusterItem>();

            for (var i = 0; i < 10; i++)
            {
                double offset = i / 60d;
                lat = lat + offset;
                lng = lng + offset;

                var item = new ClusterItem(lat, lng);
                items.Add(item);
            }

            _clusterManager.AddItems(items);
        }

        public void SetViewPoint(LatLng latlng, bool animated)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(latlng);
            builder.Zoom(14.5F);
            CameraPosition cameraPosition = builder.Build();

            if (animated)
            {
                _map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
            }
            else
            {
                _map.MoveCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
            }
        }
    }
    }


