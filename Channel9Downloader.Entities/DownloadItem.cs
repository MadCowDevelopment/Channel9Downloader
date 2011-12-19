namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a download item.
    /// </summary>
    public class DownloadItem : ObservableModel
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="IsDownloading"/> property.
        /// </summary>
        private bool _isDownloading;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is currently downloading.
        /// </summary>
        public bool IsDownloading
        {
            get
            {
                return _isDownloading;
            }

            set
            {
                _isDownloading = value;
                RaisePropertyChanged();
            }
        }

        private int _progressPercentage;

        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }

            set
            {
                _progressPercentage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the RSS item.
        /// </summary>
        public RssItem RssItem
        {
            get; set;
        }

        #endregion Public Properties
    }
}