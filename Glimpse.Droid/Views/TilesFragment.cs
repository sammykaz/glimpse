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
using System;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.TilesFragment")]
    public class TilesFragment : MvxFragment<TilesViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        RadioGroup _radioGroup;
        int _previousCheckedFilterId;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.TilesView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            // (this.Activity as MainActivity).SetCustomTitle("Tiles");
            _radioGroup = (RadioGroup)view.FindViewById(Resource.Id.filter_radiogroup);
            _radioGroup.SetOnCheckedChangeListener(this);


        }



        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            if(checkedId == 1)
            {
                ViewModel.SelectedItem = null; 
            }
            else
            {
                int checkedId0BasedIndex = checkedId - 2;
                Categories category = (Categories)checkedId0BasedIndex;
                ViewModel.SelectedItem = category;
            }
        }
    }
}