using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for accessing categories.
    /// </summary>
    public interface ICategoryRepository
    {
        #region Methods

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>Returns a list of all categories.</returns>
        Categories GetCategories();

        /// <summary>
        /// Updates the available categories.
        /// </summary>
        void UpdateCategories();

        #endregion Methods
    }
}