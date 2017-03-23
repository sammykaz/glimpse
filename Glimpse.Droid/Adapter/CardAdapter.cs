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

        public CardAdapter(Context context, int resource) :base(context,resource)
        {
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return convertView;
        }

        public override async void BindView(int position, View convertView, ViewGroup parent)
        {
            var pwl = (PromotionWithLocation)GetItem(position);
            var cardTitle = convertView.FindViewById<TextView>(Resource.Id.cardTitle);


            var cardImage = convertView.FindViewById<ImageView>(Resource.Id.cardImage);

            var cardDescription = convertView.FindViewById<TextView>(Resource.Id.cardDescription);

            if (pwl.Title != null)
                cardTitle.Text = pwl.Title;

            if (pwl.Description != null)
                cardDescription.Text = pwl.Description;

            if (pwl.Image != null)
                cardImage.SetImageBitmap(BitmapFactory.DecodeByteArray(pwl.Image, 0, pwl.Image.Length));
        }
    }
}