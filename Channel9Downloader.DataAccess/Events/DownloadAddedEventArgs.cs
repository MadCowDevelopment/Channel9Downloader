using System;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    /// <summary>
    /// This class holds information about a download added event.
    /// </summary>
    public class DownloadAddedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadAddedEventArgs"/> class.
        /// </summary>
        /// <param name="downloadItem">The added download.</param>
        public DownloadAddedEventArgs(DownloadItem downloadItem)
        {
            DownloadItem = downloadItem;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the download that has been added.
        /// </summary>
        public DownloadItem DownloadItem
        {
            get; private set;
        }

        #endregion Public Properties
    }
}