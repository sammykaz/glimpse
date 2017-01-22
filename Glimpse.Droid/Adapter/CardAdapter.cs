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

namespace Glimpse.Droid.Adapter
{
    public class CardAdapter : CardStackAdapter
    {
        public CardAdapter(Context context, int resource):base(context,resource)
        {
        }

        public override void BindView(int position, View convertView, ViewGroup parent)
        {
            ImageView imgView = convertView.FindViewById<ImageView>(Resource.Id.image_content);
            imgView.SetImageResource((int)GetItem(position));
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return convertView;
        }
    }
}