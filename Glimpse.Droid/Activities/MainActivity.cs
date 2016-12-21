
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Fragging.Fragments;
using Glimpse.Core.ViewModel;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Glimpse.Core.Services.General;
using Android.Widget;

namespace Glimpse.Droid.Activities
{
    [Activity(Label = "Main Activity", Theme = "@style/AppTheme", 
        LaunchMode = LaunchMode.SingleTop, 
        ScreenOrientation = ScreenOrientation.Portrait, 
        Name = "glimpse.droid.activities.MainActivity")]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        private DrawerLayout _drawerLayout;
        private MvxActionBarDrawerToggle _drawerToggle;
        private FragmentManager _fragmentManager;
        internal DrawerLayout DrawerLayout { get { return _drawerLayout; } }

        private static MainActivity mainActivity;

        public new MainViewModel ViewModel
        {
            get { return (MainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public static MainActivity getInstance()
        {
            return mainActivity;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (CheckAuthenticationStatus())
            {
                _fragmentManager = FragmentManager;

                SetContentView(Resource.Layout.MainView);
                mainActivity = this;

                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);

                _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                _drawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow_light, (int) GravityFlags.Start);
                _drawerToggle = new MvxActionBarDrawerToggle(this, _drawerLayout, Resource.String.drawer_open,
                    Resource.String.drawer_close);
                _drawerToggle.DrawerClosed += _drawerToggle_DrawerClosed;
                _drawerToggle.DrawerOpened += _drawerToggle_DrawerOpened;

                SupportActionBar.SetDisplayShowTitleEnabled(false);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                _drawerToggle.DrawerIndicatorEnabled = true;
                _drawerLayout.SetDrawerListener(_drawerToggle);

               ViewModel.ShowMenu();
               ViewModel.ShowMap();
            }
        }

        //uploading picture from gallery
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView = FindViewById<ImageView>(Resource.Id.imgPic);
                imageView.SetImageURI(data.Data);
            }
        }

        private void _drawerToggle_DrawerOpened(object sender, ActionBarDrawerEventArgs e)
        {
            InvalidateOptionsMenu();
        }

        private void _drawerToggle_DrawerClosed(object sender, ActionBarDrawerEventArgs e)
        {
            InvalidateOptionsMenu();
        }


        
        public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            var currentFrag = SupportFragmentManager.FindFragmentById(Resource.Id.content_frame) as MvxFragment;

            if (currentFrag != null && fragmentInfo.ViewModelType != typeof(MenuViewModel) 
                && currentFrag.FindAssociatedViewModelType() != fragmentInfo.ViewModelType)
                fragmentInfo.AddToBackStack = true;
            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }

        internal void CloseDrawerMenu()
        {
            _drawerLayout.CloseDrawers();
        }





        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (_drawerToggle.OnOptionsItemSelected(item))
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            _drawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            _drawerToggle.SyncState();
        }

        private bool CheckAuthenticationStatus()
        {
            //check your shared pref value for login in this method
            if (Settings.LoginStatus)
            {
                if (LoginActivity.getInstance() != null)
                {
                    LoginActivity.getInstance().Finish();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}