using System;
using System.Globalization;
using System.Windows.Data;

using Channel9Downloader.Entities;

namespace Channel9Downloader.Converters
{
    /// <summary>
    /// This class converts the download priority to a bool value.
    /// </summary>
    public class DownloadPriorityToBoolConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts the download priority to a bool value.
        /// </summary>
        /// <returns>
        /// If the converter parameter and download priority match it returns true, false otherwise.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var priorityValue = (DownloadPriority)value;
            var priorityParameter = (DownloadPriority)parameter;

            return priorityValue == priorityParameter;
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
            var isChecked = (bool)value;
            var priorityParameter = (DownloadPriority)parameter;

            return isChecked ? (object)priorityParameter : null;
        }

        #endregion Public Methods
    }
}