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

namespace Glimpse.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.viewPager, true)]
    [Register("glimpse.droid.views.TilesFragment")]
    public class TilesFragment : MvxFragment<TilesViewModel>, RadioGroup.IOnCheckedChangeListener
    {
        private RadioGroup _radioGroup;
        private CardStack _cardStack;
        private CardAdapter _cardAdapter;
        private Button _likeButton;
        private Button _dislikeButton;
        private readonly int _discardDistancePx;
        private List<Bitmap> _ImageResources;
        private List<byte[]> _byteImages;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
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
            _cardStack.CardEventListener = new CustomCardView.CardSwipeListener(Dp2Px(this.Context, 100), _cardStack);
            await InitializeImages();

            _cardAdapter.OnCardSwipeActionEvent += _cardAdapter_OnCardSwipeActionEvent;
            _cardStack.Adapter = _cardAdapter;

            //buttons
            _likeButton = view.FindViewById<Button>(Resource.Id.btnLike);
            _likeButton.Click += _likeButton_Click;
            _dislikeButton = view.FindViewById<Button>(Resource.Id.btnDislike);
            _dislikeButton.Click += _dislikeButton_Click;
        }

        private async Task InitializeImages()
        {
            _byteImages = (await ViewModel.GetPromotionsWithLocation()).Select(p => p.Image).ToList();
            _cardAdapter = new CardAdapter(this.Context, Resource.Layout.Card_Layout);
            foreach (byte[] image in _byteImages)
            {
                _cardAdapter.Add(new CardModel { Image = image });
            }    
        }


        private void _likeButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Context, "Like", ToastLength.Short).Show();
            (this.Activity as MainActivity).RunOnUiThread(() => _cardStack.DiscardTop(3, 1000));
        }


        private void _dislikeButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Context, "Dislike", ToastLength.Short).Show();
            (this.Activity as MainActivity).RunOnUiThread(() => _cardStack.DiscardTop(2, 1000));
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

  
        public static int Dp2Px(Context context, int dip)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dip, displayMetrics);
        }
    }
}