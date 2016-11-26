using MvvmCross.Platform.Converters;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Converters
{
    public class UploadingImageValueConverter : MvxValueConverter<MediaFile, string>
    {
        protected override string Convert(MediaFile value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return "res:"+value.AlbumPath;
            }
            else
            {
                return "@drawable/settings";
            }
        }
    }
}
