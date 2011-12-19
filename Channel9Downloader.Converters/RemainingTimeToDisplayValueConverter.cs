using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    public class RemainingTimeToDisplayValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eta = (TimeSpan) value;

            if(eta.TotalSeconds == 0)
            {
                return string.Empty;
            }

            if(eta.TotalSeconds > 86400)
            {
                return string.Format("{0} d {1} h", eta.Days, eta.Hours);
            }
            if (eta.TotalSeconds > 3600)
            {
                return string.Format("{0}:{1:00} h", eta.Hours, eta.Minutes);
            }

            if (eta.TotalSeconds > 60)
            {
                return string.Format("{0}:{1:00} mins", eta.Minutes, eta.Seconds);
            }

            return string.Format("{0} secs", eta.Seconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
