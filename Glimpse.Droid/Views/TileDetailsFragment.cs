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
using System.IO;
using System.Collections.Generic;
using Android.Graphics;


namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.TileDetailsFragment")]
    public class TileDetailsFragment : MvxFragment<TileDetailsViewModel>, ViewPager.IOnPageChangeListener
    { 
        protected View _view;
        private int _dotsCount;
        private ImageView[] _dots;
        private LinearLayout _dotsLinearLayout;
        private ViewPager _viewPager;
        private SlidingImageAdapter _adapter;
        private List<Bitmap> _ImageResources;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.TileDetailsView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Details");
            SetupViewPagerAndAdapter();
            SetupDotsControl();
        }



        public void SetupViewPagerAndAdapter()
        {
            //byte[] byteImages = ViewModel.Images;
            _ImageResources = new List<Bitmap> {BitmapFactory.DecodeResource(Resources, Resource.Raw.promotion), BitmapFactory.DecodeResource(Resources, Resource.Raw.promociones) };
            _adapter = new SlidingImageAdapter(this.Context, _ImageResources);
            _viewPager = (ViewPager)View.FindViewById(Resource.Id.imagesViewPager);
            _viewPager.Adapter = _adapter;
            _viewPager.SetOnPageChangeListener(this);
        }

        private void SetupDotsControl()
        {
            _dotsLinearLayout = (LinearLayout)View.FindViewById(Resource.Id.viewPagerCountDots);
            _dotsCount = _adapter.Count;
            _dots = new ImageView[_dotsCount];

            for (int i = 0; i < _dotsCount; i++)
            {
                _dots[i] = new ImageView(this.Context);
                _dots[i].SetImageDrawable(Resources.GetDrawable(Resource.Drawable.nonselecteditem_dot));
                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                p.SetMargins(4, 0, 4, 0);
                _dotsLinearLayout.AddView(_dots[i], p);
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



    }
}