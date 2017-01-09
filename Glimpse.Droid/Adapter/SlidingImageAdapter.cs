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
using Android.Support.V4.App;
using Android.Support.V4.View;
using Java.Lang;

namespace Glimpse.Droid.Adapter
{
    public class SlidingImageAdapter : PagerAdapter
    {

        Context _context;
        int[] _resources;

        public SlidingImageAdapter(Context context, int[] resources)
        {
            _context = context;
            _resources = resources;
        }


        public override int Count
        {
            get
            {
                return _resources.Length;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == ((LinearLayout)@object);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.pager_item, container, false);
            ImageView imageView = (ImageView)itemView.FindViewById(Resource.Id.img_pager_item);
            container.AddView(itemView);

            return itemView;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((LinearLayout)@object);
        }

    }
}