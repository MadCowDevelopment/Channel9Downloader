using System;
using System.Globalization;
using System.Windows.Data;

using Channel9Downloader.Entities;

namespace Channel9Downloader.Converters
{
    /// <summary>
    /// This class converts the summary of the selected video.
    /// If no video is selected a message that the user should select one is shown.
    /// </summary>
    public class SelectedVideoToSummaryConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rssItem = value as RssItem;

            if (rssItem == null)
            {
                return "If you select something, I can tell you more about it...";
            }

            return rssItem.Summary;
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
