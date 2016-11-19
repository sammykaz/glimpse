
using Android.App;
using Android.Content.PM;
using Android.OS;
using Glimpse.Core.ViewModel;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Glimpse.Droid.Activities
{
    [Activity(Label = "Starting Map Activity", Theme = "@style/AppTheme",
    LaunchMode = LaunchMode.SingleTop,
    ScreenOrientation = ScreenOrientation.Portrait,
    Name = "glimpse.droid.activities.StartingMapActivity")]
    public class StartingMapActivity : MvxCachingFragmentCompatActivity<StartingMapViewModel>
    {
        public new StartingMapViewModel ViewModel
        {
            get { return (StartingMapViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartingMapView);

            ViewModel.ShowStartingMap();
        }
    }
}