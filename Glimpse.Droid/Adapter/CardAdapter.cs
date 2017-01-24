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
        public CardAdapter(Context context, int resource):base(context,resource)
        {
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = ((CustomCardView)convertView);           
            view.OnCardSwipeActionEvent += action => { OnCardSwipeActionEvent?.Invoke(action); };

            return convertView;
        }
       
        public event Action<string> OnCardSwipeActionEvent;

        public override void BindView(int position, View convertView, ViewGroup parent)
        {
            var cm = (CardModel)GetItem(position);
            var imgProduct = convertView.FindViewById<ImageView>(Resource.Id.imgProduct);

            imgProduct.SetImageBitmap(BitmapFactory.DecodeByteArray(cm.Image, 0, cm.Image.Length));
        }
    }
}