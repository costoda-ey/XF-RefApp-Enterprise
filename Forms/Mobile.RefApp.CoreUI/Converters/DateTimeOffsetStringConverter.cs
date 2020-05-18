using System;
using System.Globalization;
using Xamarin.Forms;

namespace Mobile.RefApp.CoreUI.Converters
{
    public class DateTimeOffsetStringConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value is DateTimeOffset item)
                result = item.ToLocalTime().ToString();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
