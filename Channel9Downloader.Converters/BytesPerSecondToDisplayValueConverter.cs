using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    /// <summary>
    /// This class converts bytes per second to a display value.
    /// </summary>
    public class BytesPerSecondToDisplayValueConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts a double value in seconds to a more readable string representation.
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns <see langword="null"/>, 
        /// the valid <see langword="null"/> value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (double)value;

            if (bytes == 0)
            {
                return string.Empty;
            }

            if (bytes > 1000000000)
            {
                var gigs = bytes / 1000000000.0;
                return string.Format("{0:0.00} GB/s", gigs); // Yeah, I wish...
            }

            if (bytes > 1000000)
            {
                var megs = bytes / 1000000.0;
                return string.Format("{0:0.00} MB/s", megs);
            }

            if (bytes > 1000)
            {
                var kilos = bytes / 1000.0;
                return string.Format("{0:0.00} KB/s", kilos);
            }

            return string.Format("{0} Bytes/s", bytes);
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns <see langword="null"/>, 
        /// the valid <see langword="null"/> value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion Public Methods
    }
}