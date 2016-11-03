using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Glimpse.Droid.Extensions;
using Glimpse.Droid.Activities;
using MyTrains.Core.ViewModel;
using Glimpse.Droid;
using Android.OS;
using Android.App;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using MvvmCross.Droid.Support.V7.Fragging;
using MvvmCross.Binding.BindingContext;
using Glimpse.Droid.Helpers;

namespace MyTrains.Droid.Views

{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxFragmentActivity
    {
       
        private Marker _store;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);

            var viewModel = (MapViewModel)ViewModel;

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);

            var options = new MarkerOptions();
            options.SetPosition(new LatLng(viewModel.Store.Location.Lat, viewModel.Store.Location.Lng));
            options.SetTitle("Store");
            _store = mapFragment.Map.AddMarker(options);

            LatLng location = new LatLng(45.5017, -73.5673);
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(viewModel.DefaulZoom);
            builder.Bearing(viewModel.DefaultBearing);
            builder.Tilt(viewModel.DefaultTilt);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

           
            GoogleMap map = mapFragment.Map;
            if (map != null)
            {
                map.MoveCamera(cameraUpdate);
            }

            var set = this.CreateBindingSet<MapView, MapViewModel>();
            set.Bind(_store)
               .For(m => m.Position)
               .To(vm => vm.Store.Location)
               .WithConversion(new LatLngValueConverter(), null);
            set.Apply();

        }
    }
}
