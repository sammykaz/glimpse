using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform.Converters;
using System.Globalization;
using System.IO;
using Android.Graphics;
using Android.Media;

namespace Glimpse.Core.Converters
{
    public class InMemoryImageConverter : MvxValueConverter<byte[], Bitmap>
    {     

        protected override byte[] ConvertBack(Bitmap value, Type targetType, object parameter, CultureInfo culture)
        {
            var stream = new MemoryStream();

          //  value.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            return stream.ToArray();
        }
    }
}
