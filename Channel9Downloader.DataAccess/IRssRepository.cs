using System.Collections.Generic;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides access to RSS items.
    /// </summary>
    public interface IRssRepository
    {
        #region Methods

        /// <summary>
        /// Gets all RSS items for the specified category.
        /// </summary>
        /// <param name="category">The category for which RSS items should be retrieved.</param>
        /// <returns>Returns a list of all RSS items of the specified category.</returns>
        List<RssItem> GetRssItems(Category category);
        
        /// <summary>
        /// Gets all RSS items.
        /// </summary>
        /// <returns>Returns a list of all RSS items.</returns>
        List<RssItem> GetRssItems();
        
        #endregion Methods
    }
}