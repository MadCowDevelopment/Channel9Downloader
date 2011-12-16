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
        /// Downloader used for retrieving categories from the channel 9 website.
        /// </summary>
        private readonly ICategoryDownloader _categoryDownloader;

        /// <summary>
        /// Folder utils used for retrieving the user folder.
        /// </summary>
        private readonly IFolderUtils _folderUtils;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="categoryDownloader">Downloader used for retrieving categories from the channel 9 website.</param>
        /// <param name="folderUtils">Folder utils used for retrieving the user folder.</param>
        /// <param name="categoriesDataAccess">The data access for categories.</param>
        [ImportingConstructor]
        public CategoryRepository(
            ICategoryDownloader categoryDownloader,
            IFolderUtils folderUtils,
            ICategoriesDataAccess categoriesDataAccess)
        {
            _categoryDownloader = categoryDownloader;
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
            var filename = CreateFilenameForCategory();
            var categories = _categoriesDataAccess.LoadCategories(filename);

            if (categories == null)
            {
                var tags = _categoryDownloader.GetAllCategories<Tag>();
                var shows = _categoryDownloader.GetAllCategories<Show>();
                var series = _categoryDownloader.GetAllCategories<Series>();
                categories = new Categories(tags, shows, series);

                _categoriesDataAccess.SaveCategories(categories, filename);
            }

            return categories;
        }

        /// <summary>
        /// Updates the available categories.
        /// </summary>
        public void UpdateCategories()
        {
            var filename = CreateFilenameForCategory();
            var tags = _categoryDownloader.GetAllCategories<Tag>();
            var shows = _categoryDownloader.GetAllCategories<Show>();
            var series = _categoryDownloader.GetAllCategories<Series>();
            var categories = new Categories(tags, shows, series);

            _categoriesDataAccess.SaveCategories(categories, filename);
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

        #endregion Private Methods
    }
}