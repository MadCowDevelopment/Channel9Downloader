using System.ComponentModel.Composition;
using System.IO;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class is used to retrieve categories.
    /// </summary>
    [Export(typeof(ICategoryRepository))]
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields

        /// <summary>
        /// Data access used for loading/saving categories.
        /// </summary>
        private readonly ICategoriesDataAccess _categoriesDataAccess;

        /// <summary>
        /// This object serves as lock for categories.
        /// </summary>
        private readonly object _categoryLock = new object();

        /// <summary>
        /// Scraper used for retrieving categories from the channel 9 website.
        /// </summary>
        private readonly ICategoryScraper _categoryScraper;

        /// <summary>
        /// Folder utils used for retrieving the user folder.
        /// </summary>
        private readonly IFolderUtils _folderUtils;

        /// <summary>
        /// Contains all available categories.
        /// </summary>
        private Categories _categories;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="categoryScraper">Scraper used for retrieving categories from the channel 9 website.</param>
        /// <param name="folderUtils">Folder utils used for retrieving the user folder.</param>
        /// <param name="categoriesDataAccess">The data access for categories.</param>
        [ImportingConstructor]
        public CategoryRepository(
            ICategoryScraper categoryScraper,
            IFolderUtils folderUtils,
            ICategoriesDataAccess categoriesDataAccess)
        {
            _categoryScraper = categoryScraper;
            _folderUtils = folderUtils;
            _categoriesDataAccess = categoriesDataAccess;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Gets a list of all categories.
        /// </summary>
        /// <returns>Returns a list of all categories.</returns>
        public Categories GetCategories()
        {
            lock (_categoryLock)
            {
                if (_categories != null)
                {
                    return _categories;
                }

                var filename = CreateFilenameForCategory();
                _categories = _categoriesDataAccess.LoadCategories(filename);

                if (_categories == null)
                {
                    RetrieveCategories();
                }

                return _categories;
            }
        }

        /// <summary>
        /// Saves the categories.
        /// </summary>
        public void SaveCategories()
        {
            lock (_categoryLock)
            {
                var filename = CreateFilenameForCategory();
                _categoriesDataAccess.SaveCategories(_categories, filename);
            }
        }

        /// <summary>
        /// Updates the available categories.
        /// </summary>
        public void UpdateCategories()
        {
            lock (_categoryLock)
            {
                RetrieveCategories();
                SaveCategories();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the filename for the category file.
        /// </summary>
        /// <returns>Returns the filename for the category file.</returns>
        private string CreateFilenameForCategory()
        {
            var userFolder = _folderUtils.GetUserDataPath();
            var filename = Path.Combine(userFolder, "Categories.data");
            return filename;
        }

        /// <summary>
        /// Retrieves the categories from the channel 9 site.
        /// </summary>
        private void RetrieveCategories()
        {
            var tags = _categoryScraper.GetAllCategories<Tag>();
            var shows = _categoryScraper.GetAllCategories<Show>();
            var series = _categoryScraper.GetAllCategories<Series>();
            _categories = new Categories(tags, shows, series);
        }

        #endregion Private Methods
    }
}