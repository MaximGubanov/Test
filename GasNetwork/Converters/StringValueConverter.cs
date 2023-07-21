using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GasNetwork.Converters
{
    public class StringValueConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                var startString = 0;
                var endString = int.Parse(parameter as string);
                var representedValue = value.ToString().Trim();

                if (representedValue.Length >= endString)
                {
                    return $"{representedValue.ToString().Substring(startString, endString)}...";
                }

                return representedValue;
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}