using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    public class BytesToDisplayValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (long) value;

            if (bytes == 0)
            {
                return string.Empty;
            }

            if (bytes > 1000000000)
            {
                var gigs = bytes/1000000000.0;
                return string.Format("{0:0.00} GB", gigs);
            }
            if (bytes > 1000000)
            {
                var megs = bytes/1000000.0;
                return string.Format("{0:0.00} MB", megs);
            }

            if (bytes > 1000)
            {
                var kilos = bytes/1000.0;
                return string.Format("{0:0.00} KB", kilos);
            }

            return string.Format("{0} Bytes", bytes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
