using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;

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
        ListCollectionView Downloads
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is downloading.
        /// </summary>
        bool IsDownloading
        {
            get; set;
        }

        /// <summary>
        /// Gets a command to start downloads.
        /// </summary>
        ICommand StartDownloadsCommand
        {
            get;
        }

        /// <summary>
        /// Gets a command to stop downloads.
        /// </summary>
        ICommand StopDownloadsCommand
        {
            get;
        }

        /// <summary>
        /// Gets a command to update the available downloads.
        /// </summary>
        ICommand UpdateDownloadsCommand
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