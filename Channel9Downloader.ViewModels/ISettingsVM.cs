using System.Windows.Input;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Configuration;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This interface is used for the <see cref="SettingsVM"/>.
    /// </summary>
    public interface ISettingsVM : ISimpleViewModel
    {
        #region Properties

        /// <summary>
        /// Gets a command to browse for the download folder.
        /// </summary>
        ICommand BrowseCommand
        {
            get;
        }

        /// <summary>
        /// Gets or sets the open folder service.
        /// </summary>
        IOpenFolderService OpenFolderService
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        Settings Settings
        {
            get; set;
        }

        #endregion Properties
    }
}