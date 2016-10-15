using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using Glimpse.Core.ViewModel;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Glimpse.Droid.Extensions;
using Square.TimesSquare;
using Glimpse.Droid.Activities;
using MyTrains.Core.ViewModel;
using Glimpse.Droid;

namespace MyTrains.Droid.Views
{
    [MvxFragment(typeof(Glimpse.Core.ViewModel.MainViewModel), Resource.Id.content_frame, true)]
    [Register("mytrains.droid.views.VendorSignUpFragment")]
    public class VendorSignUpFragment : MvxFragment<VendorSignUpViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.VendorSignUpView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Vendor Sign Up");
        }

        public override void OnStart()
        {
            base.OnStart();

          
        }

    }
}