using System.Collections.Generic;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for retrieving different categories.
    /// </summary>
    public interface IChannel9CategoryBrowser
    {
        #region Methods

        /// <summary>
        /// Gets a list of all available series.
        /// </summary>
        /// <returns>Returns a list of all available series.</returns>
        List<Series> GetAllSeries();

        /// <summary>
        /// Gets a list of all available shows.
        /// </summary>
        /// <returns>Returns a list of all available shows.</returns>
        List<Show> GetAllShows();

        /// <summary>
        /// Gets a list of all available tags.
        /// </summary>
        /// <returns>Returns a list of all available tags.</returns>
        List<Tag> GetAllTags();

        #endregion Methods
    }
}