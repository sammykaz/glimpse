using System;
using System.Globalization;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using Glimpse.Core.Contracts.Services;

namespace Glimpse.Core.Converters
{
    public class CurrencyToStringConverter: MvxValueConverter<double, string>
    {
        protected override string Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            var service = Mvx.Resolve<ISettingsDataService>();
            return service.GetActiveCurrency().CurrencySymbol + " " + value;
        }
    }
}