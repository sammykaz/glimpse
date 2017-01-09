using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Glimpse.Core.ViewModel;
using Glimpse.Droid.Activities;
using Glimpse.Droid.Extensions;
using Android.Widget;
using Glimpse.Droid.Adapter;
using Android.Support.V4.View;
using System;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.TileDetailsFragment")]
    public class TileDetailsFragment : MvxFragment<SettingsViewModel>, ViewPager.IOnPageChangeListener, View.IOnClickListener
    { 
        protected View _view;
        private ImageButton btnNext, btnFinish;
        private int _dotsCount;
        private ImageView[] dots;
        private LinearLayout pagerIndicator;
        private ViewPager _introImages;
        private SlidingImageAdapter _adapter;
        private int[] _ImageResources =
        {
            Resource.Raw.Yoshi,
            Resource.Raw.mario
        };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.TileDetailsView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Details");
        }



        public void SetReference()
        {
            _introImages = (ViewPager)View.FindViewById(Resource.Id.pager_introduction);
            pagerIndicator = (LinearLayout)View.FindViewById(Resource.Id.viewPagerCountDots);
            _adapter = new SlidingImageAdapter(this.Context, _ImageResources);
            _introImages.Adapter = _adapter;
            _introImages.SetOnPageChangeListener(this);
            setUIPageViewController();

        }

        private void setUIPageViewController()
        {
            throw new NotImplementedException();
        }

        public void OnPageScrollStateChanged(int state)
        {
            throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}