using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for serializing/deserializing categories.
    /// </summary>
    public interface ICategoriesDataAccess
    {
        #region Methods

        /// <summary>
        /// Load categories from the specified file.
        /// </summary>
        /// <param name="filename">Name of the file that contains categories.</param>
        /// <returns>Returns the categories read from the file.</returns>
        Categories LoadCategories(string filename);

        /// <summary>
        /// Saves categories to the specified file.
        /// </summary>
        /// <param name="categories">Categories that should be saved.</param>
        /// <param name="filename">Name of the file that should be written.</param>
        void SaveCategories(Categories categories, string filename);

        #endregion Methods
    }
}