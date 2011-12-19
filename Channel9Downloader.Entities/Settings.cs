using System;
using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about the application settings.
    /// </summary>
    [DataContract]
    public class Settings : ObservableModel
    {
        #region Fields

        /// <summary>
        /// Property name for <see cref="MaximumParallelDownloads"/> property.
        /// </summary>
        public const string PROP_MAXIMUM_PARALLEL_DOWNLOADS = "MaximumParallelDownloads";

        /// <summary>
        /// Backing field for <see cref="DownloadFolder"/> property.
        /// </summary>
        private string _downloadFolder;

        /// <summary>
        /// Backing field for <see cref="MaximumParallelDownloads"/> property.
        /// </summary>
        private int _maximumParallelDownloads;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            DownloadFolder = string.Empty;
            MaximumParallelDownloads = 2;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the folder where movies will be downloaded to.
        /// </summary>
        [DataMember]
        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
            }

            set
            {
                _downloadFolder = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number of parallel downloads.
        /// </summary>
        [DataMember]
        public int MaximumParallelDownloads
        {
            get
            {
                return _maximumParallelDownloads;
            }

            set
            {
                _maximumParallelDownloads = value;
                RaisePropertyChanged(PROP_MAXIMUM_PARALLEL_DOWNLOADS);
            }
        }

        #endregion Public Properties
    }
}