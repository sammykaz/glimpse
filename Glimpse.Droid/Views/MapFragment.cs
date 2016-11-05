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
            var view = inflater.Inflate(Resource.Layout.MapView, null);
            _mapView = view.FindViewById<MapView>(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);
            return view ;
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
                var viewModel = (MapViewModel)ViewModel;

                var options = new MarkerOptions();
                options.SetPosition(new LatLng(viewModel.Store.Location.Lat, viewModel.Store.Location.Lng));
                options.SetTitle("Store");
                options.SetAlpha(0.7f);
                options.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
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
