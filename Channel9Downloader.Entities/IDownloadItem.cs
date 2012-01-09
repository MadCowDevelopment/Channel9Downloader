using System;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This interface provides methods ands properties for download items.
    /// </summary>
    public interface IDownloadItem : IObservableModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the average bytes per second download speed.
        /// </summary>
        double BytesPerSecond
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the number of bytes received.
        /// </summary>
        long BytesReceived
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        Category Category
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current download state.
        /// </summary>
        DownloadState DownloadState
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the download priority.
        /// </summary>
        DownloadPriority Priority
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the download progress in percent.
        /// </summary>
        int ProgressPercentage
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the remaining time in seconds.
        /// </summary>
        TimeSpan RemainingTime
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the RSS item.
        /// </summary>
        RssItem RssItem
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the total size of the download.
        /// </summary>
        long TotalBytesToReceive
        {
            get; set;
        }

        #endregion Properties
    }
}