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
    [MvxFragment(typeof(Glimpse.Core.ViewModel.MainViewModel), Resource.Id.content_frame, true)]
    [Register("mytrains.droid.views.MapFragment")]
    public class MapFragment : MvxFragment<MapViewModel>
    {
        private Marker _first;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.MapView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Map View");
        }

        public override void OnStart()
        {
            base.OnStart();
        }

    
    }
}