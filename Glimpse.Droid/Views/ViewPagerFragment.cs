using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Glimpse.Core.ViewModel;
using Glimpse.Droid.Activities;
using Glimpse.Droid.Extensions;
using Glimpse.Droid.Adapter;
using System.Collections.Generic;
using Android.Support.V4.View;
using static Android.Support.V4.View.ViewPager;
using System;
using Glimpse.Droid.Controls;
using Android.Support.Design.Widget;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.ViewPagerFragment")]
    public class ViewPagerFragment : MvxFragment<ViewPagerViewModel>, IOnPageChangeListener, MainActivity.OnBackPressedListener
    {
        private CustomViewPager _viewPager;
        private TabLayout _tabLayout;
        private MvxViewPagerFragmentAdapter _adapter;
        private int[] _tabIcons = { Android.Resource.Drawable.IcMediaPause, Android.Resource.Drawable.IcMediaPause, Android.Resource.Drawable.IcMediaPause };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ViewPagerView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).setOnBackPressedListener(this);
            (this.Activity as MainActivity).SetCustomTitle("Map");

            var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo>
                  {
                     new MvxViewPagerFragmentAdapter.FragmentInfo
                    {
                      FragmentType = typeof(Views.MapFragment),
                      ViewModel = ViewModel.MapViewModel
                     },
                    new MvxViewPagerFragmentAdapter.FragmentInfo
                    {
                      FragmentType = typeof(Views.CardFragment),
                      ViewModel = ViewModel.CardViewModel
                    },
                     new MvxViewPagerFragmentAdapter.FragmentInfo
                    {
                      FragmentType = typeof(Views.LikedPromotionsFragment),
                      Title = "Fragment3",
                      ViewModel = ViewModel.LikedPromotionsViewModel
                    }
                  };

            _viewPager = View.FindViewById<CustomViewPager>(Resource.Id.viewPager);
            _adapter = new MvxViewPagerFragmentAdapter(this.Context, ChildFragmentManager, fragments);
            _viewPager.Adapter = _adapter;
            _viewPager.AddOnPageChangeListener(this);
            _tabLayout = View.FindViewById<TabLayout>(Resource.Id.tabs);
            _tabLayout.SetupWithViewPager(_viewPager);

            _tabLayout.GetTabAt(0).SetIcon(_tabIcons[0]);
            _tabLayout.GetTabAt(1).SetIcon(_tabIcons[1]);
        }

        public void OnPageScrollStateChanged(int state)
        {
           // throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }

        public async void OnPageSelected(int position)
        {
            if (position == 0)
                (this.Activity as MainActivity).SetCustomTitle("Map");
            else if (position == 1)
            {
                (this.Activity as MainActivity).SetCustomTitle("CardView");
                 await ViewModel.CardViewModel.ReloadAsync();
            }
        }

        public void doBack()
        {

            if (_viewPager.CurrentItem == 1 && _viewPager.IsShown)
            {
                _viewPager.SetCurrentItem(0, true);
            }
            else
            {
                (this.Activity as MainActivity).setOnBackPressedListener(null);
                (this.Activity as MainActivity).OnBackPressed();
            }
        }

    }
}