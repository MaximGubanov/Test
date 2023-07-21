using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Reflection.Metadata;

namespace GasNetwork.Converters;

public class DateValueConverter : IValueConverter
{
    public static readonly DateValueConverter Instance = new();
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            if (parameter is not null)
            {
                return dateTime.ToString($"{parameter}");
            }
            
            return dateTime.ToString() + "ghgjhjhgjghgj";
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}