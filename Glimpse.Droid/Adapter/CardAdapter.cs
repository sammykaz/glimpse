using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gemslibe.Xamarin.Droid.UI.SwipeCards;
using Glimpse.Core.Model;
using Glimpse.Droid.Controls;
using Android.Graphics;

namespace Glimpse.Droid.Adapter
{
    public class CardAdapter : CardStackAdapter
    {
        View _parentFragmentView;
        public CardAdapter(Context context, int resource, View parentFragmentView) :base(context,resource)
        {
            _parentFragmentView = parentFragmentView;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var likeButton =  _parentFragmentView.FindViewById<Button>(Resource.Id.btnLike);
            var dislikeButton = _parentFragmentView.FindViewById<Button>(Resource.Id.btnDislike);
            likeButton.Click += (sender, args) => { OnTapButtonsEvent?.Invoke("Like"); };

            dislikeButton.Click += (sender, args) => { OnTapButtonsEvent?.Invoke("Dislike"); };


            return convertView;
        }
        public event Action<string> OnTapButtonsEvent;
        public event Action<string> OnCardSwipeActionEvent;

        public override void BindView(int position, View convertView, ViewGroup parent)
        {
            var pwlm = (PromotionWithLocation)GetItem(position);
            var imgProduct = convertView.FindViewById<ImageView>(Resource.Id.imgProduct);

            imgProduct.SetImageBitmap(BitmapFactory.DecodeByteArray(pwlm.Image, 0, pwlm.Image.Length));
        }
    }
}