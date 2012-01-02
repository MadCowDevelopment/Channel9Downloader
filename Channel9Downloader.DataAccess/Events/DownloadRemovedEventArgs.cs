using System;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    /// <summary>
    /// This class contains information about a download removed event.
    /// </summary>
    public class DownloadRemovedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadRemovedEventArgs"/> class.
        /// </summary>
        /// <param name="downloadItem">The download that has been removed.</param>
        public DownloadRemovedEventArgs(IDownloadItem downloadItem)
        {
            DownloadItem = downloadItem;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the download that has been removed.
        /// </summary>
        public IDownloadItem DownloadItem
        {
            get; set;
        }

        #endregion Public Properties
    }
}