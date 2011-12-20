using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Windows.Input;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Configuration
{
    /// <summary>
    /// This class manages the settings view.
    /// </summary>
    [Export(typeof(ISettingsVM))]
    public class SettingsVM : SimpleViewModel, ISettingsVM
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="BrowseCommand"/> property.
        /// </summary>
        private ICommand _browseCommand;

        /// <summary>
        /// Backing field for <see cref="Settings"/> property.
        /// </summary>
        private Settings _settings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsVM"/> class.
        /// </summary>
        /// <param name="settings">The settings that should be manipulated.</param>
        [ImportingConstructor]
        public SettingsVM(Settings settings)
        {
            Settings = settings;

            DisplayName = "Settings";
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a command to browse for the download folder.
        /// </summary>
        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ?? (_browseCommand = new RelayCommand(p => OnBrowse()));
            }
        }

        /// <summary>
        /// Gets or sets the open folder service.
        /// </summary>
        [Import]
        public IOpenFolderService OpenFolderService
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public Settings Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                _settings = value;
                RaisePropertyChanged(() => Settings);
            }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Browses for the download folder.
        /// </summary>
        private void OnBrowse()
        {
            if (OpenFolderService.ShowDialog() == DialogResult.OK)
            {
                Settings.DownloadFolder = OpenFolderService.SelectedPath;
            }
        }

        #endregion Private Methods
    }
}