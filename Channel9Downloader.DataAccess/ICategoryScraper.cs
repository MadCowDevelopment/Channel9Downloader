using System.Collections.Generic;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for retrieving different categories.
    /// </summary>
    public interface ICategoryScraper
    {
        #region Methods

        /// <summary>
        /// Gets a list of all categories of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the category.</typeparam>
        /// <returns>Returns a list of all categories of the specified type.</returns>
        List<T> GetAllCategories<T>()
            where T : Category;

        #endregion Methods
    }
}