using System.Collections.Generic;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for accessing finished downloads.
    /// </summary>
    public interface IFinishedDownloadsRepository
    {
        #region Methods

        /// <summary>
        /// Adds a finished download to the repository.
        /// </summary>
        /// <param name="rssItem">The item to add.</param>
        void AddFinishedDownload(RssItem rssItem);

        /// <summary>
        /// Gets a list of all finished downloads.
        /// </summary>
        /// <returns>Returns a list of all finished downloads.</returns>
        List<RssItem> GetAllFinishedDownloads();

        #endregion Methods
    }
}