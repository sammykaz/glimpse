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
    public class TileDetailsFragment : MvxFragment<TileDetailsViewModel>, ViewPager.IOnPageChangeListener, View.IOnClickListener
    { 
        protected View _view;
        private int _dotsCount;
        private ImageView[] _dots;
        private LinearLayout pagerIndicator;
        private ViewPager _viewPager;
        private SlidingImageAdapter _adapter;
        private int[] _ImageResources =
        {
             Resource.Drawable.Bart,
            Resource.Drawable.bk,
           
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
            SetReference();
        }



        public void SetReference()
        {
            pagerIndicator = (LinearLayout)View.FindViewById(Resource.Id.viewPagerCountDots);
            _viewPager = (ViewPager)View.FindViewById(Resource.Id.imagesViewPager);
            _adapter = new SlidingImageAdapter(this.Context, _ImageResources);
            _viewPager.Adapter = _adapter;
            _viewPager.SetOnPageChangeListener(this);
            setUIPageViewController();

        }

        private void setUIPageViewController()
        {
            _dotsCount = _adapter.Count;
            _dots = new ImageView[_dotsCount];

            for (int i = 0; i < _dotsCount; i++)
            {
                _dots[i] = new ImageView(this.Context);
                _dots[i].SetImageDrawable(Resources.GetDrawable(Resource.Drawable.nonselecteditem_dot));
                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                p.SetMargins(4, 0, 4, 0);
                pagerIndicator.AddView(_dots[i], p);
            }
            _dots[0].SetImageDrawable(Resources.GetDrawable(Resource.Drawable.selecteditem_dot));
        }

        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
           // throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            for (int i = 0; i < _dotsCount; i++)
            {
                _dots[i].SetImageDrawable(Resources.GetDrawable(Resource.Drawable.nonselecteditem_dot));
            }
            _dots[position].SetImageDrawable(Resources.GetDrawable(Resource.Drawable.selecteditem_dot));
        }

        public void OnClick(View v)
        {
           // throw new NotImplementedException();
        }
    }
}