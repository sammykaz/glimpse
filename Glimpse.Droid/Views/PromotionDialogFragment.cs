using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Glimpse.Core.ViewModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Glimpse.Droid.Views
{
    public class PromotionDialogFragment : DialogFragment
    {

   
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.PromotionDialogView, container, false);
            return view;
        }
    }
}