using System;

using Channel9Downloader.DataAccess.Events;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for downloading videos.
    /// </summary>
    public interface IDownloadManager
    {
        #region Events

        /// <summary>
        /// This event is raised when a new download has been added.
        /// </summary>
        event EventHandler<DownloadAddedEventArgs> DownloadAdded;

        #endregion Events

        #region Methods

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        void Initialize(Settings settings);

        #endregion Methods
    }
}