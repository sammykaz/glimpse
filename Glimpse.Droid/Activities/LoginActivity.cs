using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Glimpse.Core.ViewModel;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;

namespace Glimpse.Droid.Activities
{
    [Activity(Label = "Login Activity", Theme = "@style/AppTheme",
       LaunchMode = LaunchMode.SingleTop,
       ScreenOrientation = ScreenOrientation.Portrait,
       Name = "glimpse.droid.activities.LoginActivity")]
    public class LoginActivity : MvxCachingFragmentCompatActivity<LoginMainViewModel>
    {
        public new LoginMainViewModel ViewModel
        {
            get { return (LoginMainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginMainView);

            ViewModel.ShowLoginPage();

        }

        public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            var currentFrag = SupportFragmentManager.FindFragmentById(Resource.Id.login_content) as MvxFragment;

            if (currentFrag != null && currentFrag.FindAssociatedViewModelType() != fragmentInfo.ViewModelType)
                fragmentInfo.AddToBackStack = true;
            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }
    }
}