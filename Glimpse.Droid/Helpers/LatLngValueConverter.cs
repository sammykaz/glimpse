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
using Android.Gms.Maps.Model;
using MvvmCross.Platform.Converters;

namespace Glimpse.Droid.Helpers
{
    class LatLngValueConverter : MvxValueConverter<Core.Model.Location, LatLng>
    {
        protected override LatLng Convert(Core.Model.Location value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new LatLng(value.Lat, value.Lng);
        }

        protected override Core.Model.Location ConvertBack(LatLng value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Core.Model.Location() { Lat = value.Latitude, Lng = value.Longitude };
        }
    }
}