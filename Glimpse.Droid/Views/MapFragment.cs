using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Glimpse.Droid.Extensions;
using Glimpse.Droid.Activities;
using Glimpse.Core.ViewModel;
using Glimpse.Droid;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using MvvmCross.Binding.BindingContext;
using Glimpse.Droid.Helpers;
using System;
using MvvmCross.Binding.Droid.BindingContext;

namespace Glimpse.Droid.Views

{
    [MvxFragment(typeof(Glimpse.Core.ViewModel.MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.MapFragment")]
    public class MapFragment : MvxFragment<MapViewModel>
    {
        private MapView _mapView;
        private GoogleMap _map;
        private Marker _store;


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
            (this.Activity as MainActivity).SetCustomTitle("MapView");
            MapsInitializer.Initialize(Activity);   
        }

        public override void OnStart()
        {
            base.OnStart();
            InitializeMapAndHandlers();
        }

        private void InitializeMapAndHandlers()
        {
            SetUpMapIfNeeded();

           if (_map != null)
            {              
                _map.AddCircle(new CircleOptions()
                .InvokeCenter(new LatLng(45.5017, -73.5673))
                .InvokeRadius(5)
                .InvokeStrokeColor(Color.Red)
                .InvokeFillColor(Color.Blue));
                _map.UiSettings.MapToolbarEnabled = true;
                _map.UiSettings.ZoomControlsEnabled = true;
                _map.UiSettings.CompassEnabled = true;
                _map.UiSettings.MyLocationButtonEnabled = true;
                _map.UiSettings.RotateGesturesEnabled = true;
                _map.UiSettings.ZoomGesturesEnabled = true;
                _map.BuildingsEnabled = true; 

                var viewModel = (MapViewModel)ViewModel;

               // var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
                    
                var options = new MarkerOptions();
                options.SetPosition(new LatLng(viewModel.Store.Location.Lat, viewModel.Store.Location.Lng));
                options.SetTitle("Store");
                options.SetAlpha(0.7f);
                options.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta));
                options.InfoWindowAnchor(0.7f, 0.7f);           
                options.SetSnippet("This is the displayed store");

                _store = _map.AddMarker(options);              


                LatLng location = new LatLng(45.5017, -73.5673);
                CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                builder.Target(location);
                builder.Zoom(viewModel.DefaulZoom);
                builder.Bearing(viewModel.DefaultBearing);
                builder.Tilt(viewModel.DefaultTilt);
                CameraPosition cameraPosition = builder.Build();
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                _map.MoveCamera(cameraUpdate); 

                var set = this.CreateBindingSet<MapFragment, MapViewModel>();
                set.Bind(_store)
                   .For(m => m.Position)
                   .To(vm => vm.Store.Location)
                   .WithConversion(new LatLngValueConverter(), null).TwoWay();
                set.Apply();


            }
        }



        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _mapView.OnDestroy();
            _mapView = null;
            _map = null;
            _store = null;
        }

        public override void OnSaveInstanceState(Bundle outState)

        {
            base.OnSaveInstanceState(outState);
            _mapView.OnSaveInstanceState(outState);
        }

        public override void OnResume()

        {
            base.OnResume();
            SetUpMapIfNeeded();
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
            if ( _map== null)
            {
                _map = View.FindViewById<MapView>(Resource.Id.map).Map;
            }
        }
    }

}
