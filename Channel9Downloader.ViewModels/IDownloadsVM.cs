using System.Collections.ObjectModel;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This interface is for the downloads viewmodel.
    /// </summary>
    public interface IDownloadsVM : IBaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets the downloads.
        /// </summary>
        ObservableCollection<DownloadItem> Downloads
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes the downloads.
        /// </summary>
        /// <param name="settings">The application settings.
        /// </param>
        void Initialize(Settings settings);

        #endregion Methods
    }
}