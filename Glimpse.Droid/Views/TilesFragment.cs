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
using Glimpse.Droid.Helpers;
using Glimpse.Droid.Controls;
using Android.Util;
using Android.Content;
using System.Threading.Tasks;
using Android.Graphics;
using System.Linq;
using Glimpse.Droid.Controls.Listener;

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.TilesFragment")]
    public class TilesFragment : MvxFragment<TilesViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        private RadioGroup _radioGroup;
        private CardStack _cardStack;
        private CardAdapter _cardAdapter;
        private CustomViewPager _viewPager;
        private List<PromotionWithLocation> _promotionWithLocationList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _viewPager = (CustomViewPager)container;
            return this.BindingInflate(Resource.Layout.CardSwipeView, null);
        }

        public override async void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            // (this.Activity as MainActivity).SetCustomTitle("Tiles");
            // _radioGroup = (RadioGroup)view.FindViewById(Resource.Id.filter_radiogroup);
            // _radioGroup.SetOnCheckedChangeListener(this);

            _cardStack = (this.Activity as MainActivity).FindViewById<CardStack>(Resource.Id.card_stack);
            _cardStack.ContentResource = Resource.Layout.Card_Layout;

            //Initializing the discard distance in pixels from the origin of the card stack.
            _cardStack.CardEventListener = new CardSwipeListener(DpToPx(this.Context, 100), _cardStack, _viewPager);
            await InitializeImages();

            _cardAdapter.OnCardSwipeActionEvent += _cardAdapter_OnCardSwipeActionEvent;
            _cardAdapter.OnTapButtonsEvent += _cardAdapter_OnTapButtonsEvent;
            _cardStack.Adapter = _cardAdapter;

            
        
        }

        private async Task InitializeImages()
        {
            _promotionWithLocationList = await ViewModel.GetPromotionsWithLocation();
            _cardAdapter = new CardAdapter(this.Context, Resource.Layout.Card_Layout, this.View);
            foreach (PromotionWithLocation promo in _promotionWithLocationList)
            {
                _cardAdapter.Add(new CardModel { Image = promo.Image, PromotionId = promo.PromotionId });
            }
        }


        private void _cardAdapter_OnTapButtonsEvent(string action)
        {
            if (_cardStack.TopView != null)
            {
                Toast.MakeText(this.Context, action, ToastLength.Short).Show();
                var direction = (action == "Like") ? 3 : 2;
                Task.Delay(250).ContinueWith(o => { (this.Activity as MainActivity).RunOnUiThread(() => _cardStack.DiscardTop(direction, 700)); });
            }
            else
                Toast.MakeText(this.Context, "No cards available", ToastLength.Short).Show();
        }


        private void _cardAdapter_OnCardSwipeActionEvent(string action)
        {
            Toast.MakeText(this.Context, action, ToastLength.Short).Show();
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

  
        public int DpToPx(Context context, int dip)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dip, displayMetrics);
        }
    }
}