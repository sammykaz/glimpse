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

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("glimpse.droid.views.ViewPagerFragment")]
    public class ViewPagerFragment : MvxFragment<ViewPagerViewModel>, IOnPageChangeListener
    {

        private ViewPager _viewPager;
        private MvxViewPagerFragmentAdapter _adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ViewPagerView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Map");

            var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo>
                  {
                    new MvxViewPagerFragmentAdapter.FragmentInfo
                    {
                      FragmentType = typeof(Views.MapFragment),
                      Title = "Fragment1",
                      ViewModel = ViewModel.MapViewModel
                        },
                    new MvxViewPagerFragmentAdapter.FragmentInfo
                    {
                      FragmentType = typeof(Views.TilesFragment),
                      Title = "Fragment2",
                      ViewModel = ViewModel.TilesViewModel
                    }
                  };

            _viewPager = View.FindViewById<ViewPager>(Resource.Id.viewPager);
            _adapter = new MvxViewPagerFragmentAdapter(this.Context, ChildFragmentManager, fragments);
            _viewPager.Adapter = _adapter;
            _viewPager.AddOnPageChangeListener(this);
          /*  _viewPager.AddOnPageChangeListener(new SimpleOnPageChangeListener()
            {
                public override 
                 
             });*/
        }

        public void OnPageScrollStateChanged(int state)
        {
           // throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            if(position == 0)
            (this.Activity as MainActivity).SetCustomTitle("Map");
            else if (position == 1)
                (this.Activity as MainActivity).SetCustomTitle("Tiles");
        }
    }
}