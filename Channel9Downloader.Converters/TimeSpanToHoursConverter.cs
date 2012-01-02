using System;
using System.Globalization;
using System.Windows.Data;

namespace Channel9Downloader.Converters
{
    /// <summary>
    /// This class converts a time span to hours.
    /// </summary>
    public class TimeSpanToHoursConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts a time span to hours.
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
            var timespan = (TimeSpan)value;

            return timespan.TotalHours;
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
            var hours = (int)value;

            return TimeSpan.FromHours(hours);
        }

        #endregion
    }
}
