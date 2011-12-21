using System;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    /// <summary>
    /// This class contains information about a download removed event.
    /// </summary>
    public class DownloadRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadRemovedEventArgs"/> class.
        /// </summary>
        /// <param name="downloadItem">The download that has been removed.</param>
        public DownloadRemovedEventArgs(DownloadItem downloadItem)
        {
            DownloadItem = downloadItem;
        }

        /// <summary>
        /// Gets or sets the download that has been removed.
        /// </summary>
        public DownloadItem DownloadItem { get; set; }
    }
}
