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
using Glimpse.Droid.Adapter;
using Gemslibe.Xamarin.Droid.UI.SwipeCards;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.TilesFragment")]
    public class TilesFragment : MvxFragment<TilesViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        RadioGroup _radioGroup;
        private CardStack _cardStack;
        private CardAdapter _cardAdapter;
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.CardSwipeView, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            // (this.Activity as MainActivity).SetCustomTitle("Tiles");
            // _radioGroup = (RadioGroup)view.FindViewById(Resource.Id.filter_radiogroup);
            // _radioGroup.SetOnCheckedChangeListener(this);
            InitializeImages();
            _cardStack = (this.Activity as MainActivity).FindViewById<CardStack>(Resource.Id.card_stack);
            _cardStack.ContentResource = Resource.Layout.Card_Layout;
            _cardStack.StackMargin = 20;
            _cardStack.Adapter = _cardAdapter;
        }

        private void InitializeImages()
        {
            _cardAdapter = new CardAdapter(this.Context, 0);
            _cardAdapter.Add(Resource.Raw.promociones);
            _cardAdapter.Add(Resource.Raw.promotion);
            _cardAdapter.Add(Resource.Raw.promociones);
        }

        public void OnCheckedChanged(RadioGroup group, int checkedId)
        {
            //radio group index is based on 1, making base 0
            checkedId = checkedId - 1;
            //the filter on previous page made this checkedID increment by 7...
            checkedId = checkedId % 7;
            if(checkedId == 0)
            {
                ViewModel.SelectedItem = null; 
            }
            else
            {
                int checkedId0BasedIndex = checkedId - 1;
                Categories category = (Categories)checkedId0BasedIndex;
                ViewModel.SelectedItem = category;
            }
        }
    }
}