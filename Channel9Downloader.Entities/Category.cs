namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class serves as base class for different video categories.
    /// </summary>
    public class Category
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the relative path for the category.
        /// </summary>
        public string RelativePath
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the title of the category.
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this category is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get; set;
        }

        #endregion Public Properties
    }
}