using System.ComponentModel.Composition;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class serializes/deserializes categories to a file.
    /// </summary>
    [Export(typeof(ICategoriesDataAccess))]
    public class CategoriesDataAccess : DataContractDataAccess<Categories>, ICategoriesDataAccess
    {
        #region Public Methods

        /// <summary>
        /// Load categories from the specified file.
        /// </summary>
        /// <param name="filename">Name of the file that contains categories.</param>
        /// <returns>Returns the categories read from the file.</returns>
        public Categories LoadCategories(string filename)
        {
            return Load(filename);
        }

        /// <summary>
        /// Saves categories to the specified file.
        /// </summary>
        /// <param name="categories">Categories that should be saved.</param>
        /// <param name="filename">Name of the file that should be written.</param>
        public void SaveCategories(Categories categories, string filename)
        {
            Save(categories, filename);
        }

        #endregion Public Methods
    }
}