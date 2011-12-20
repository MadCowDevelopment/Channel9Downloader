using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    /// <summary>
    /// This class converts the remaining time in seconds to a more readable format.
    /// </summary>
    public class RemainingTimeToDisplayValueConverter : IValueConverter
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
            var eta = (TimeSpan)value;

            if (eta.TotalSeconds == 0)
            {
                return string.Empty;
            }

            if (eta.TotalSeconds > 86400)
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