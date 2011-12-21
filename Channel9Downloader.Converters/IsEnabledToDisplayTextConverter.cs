using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    public class IsEnabledToDisplayTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isEnabled = (bool) value;
            return isEnabled ? "Enabled" : "Disabled";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
