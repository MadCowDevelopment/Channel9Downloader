using System.Collections.Generic;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for downloading videos.
    /// </summary>
    public interface IDownloadManager
    {
        #region Properties

        /// <summary>
        /// Gets the download queue.
        /// </summary>
        Queue<DownloadItem> Downloads
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        void Initialize(Settings settings);

        #endregion Methods
    }
}