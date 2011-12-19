using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

using Channel9Downloader.Composition;
using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Categories;
using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;
using Channel9Downloader.ViewModels.Ribbon;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class provides logic and binding for the main window.
    /// </summary>
    [Export(typeof(IMainWindowVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainWindowVM : SimpleViewModel, IMainWindowVM
    {
        #region Fields

        /// <summary>
        /// The composer used for dependency injection.
        /// </summary>
        private readonly IDependencyComposer _composer;

        /// <summary>
        /// Backing field for <see cref="ContentArea"/> property.
        /// </summary>
        private IBaseViewModel _contentArea;

        /// <summary>
        /// Backing field for <see cref="ShowSettingsViewCommand"/> property.
        /// </summary>
        private ICommand _showSettingsViewCommand;

        /// <summary>
        /// The application settings.
        /// </summary>
        private Settings _settings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVM"/> class.
        /// </summary>
        /// <param name="composer">The composer used for dependency injection.</param>
        [ImportingConstructor]
        public MainWindowVM(IDependencyComposer composer)
        {
            _composer = composer;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the viewmodel for the main content.
        /// </summary>
        public IBaseViewModel ContentArea
        {
            get
            {
                return _contentArea;
            }

            private set
            {
                _contentArea = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the viewmodel for the ribbon bar.
        /// </summary>
        [Import]
        public IRibbonVM RibbonBar
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets or sets the dialog service.
        /// </summary>
        [Import]
        public IDialogService DialogService { get; set; }

        /// <summary>
        /// Gets or sets the settings manager.
        /// </summary>
        [Import]
        public ISettingsManager SettingsManager { get; set; }

        /// <summary>
        /// Gets a command to show the settings view.
        /// </summary>
        public ICommand ShowSettingsViewCommand
        {
            get
            {
                return _showSettingsViewCommand ??
                       (_showSettingsViewCommand = new RelayCommand(p => OnShowSettingsView()));
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this viewmodel.
        /// </summary>
        public void Initialize()
        {
            InitializeRibbon();
            InitializeSettings();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Initializes the ribbon.
        /// </summary>
        private void InitializeRibbon()
        {
            RibbonBar.Initialize();
            RibbonBar.SelectedTabChanged += RibbonBarSelectedTabChanged;
        }

        /// <summary>
        /// Initializes the application settings.
        /// </summary>
        private void InitializeSettings()
        {
            _settings = SettingsManager.LoadSettings();
            _composer.ComposeExportedValue(_settings);
        }

        /// <summary>
        /// Requests the settings view.
        /// </summary>
        private void OnShowSettingsView()
        {
            var settingsVM = _composer.GetExportedValue<ISettingsVM>();
            DialogService.ShowDialog(settingsVM.DisplayName, settingsVM);
            SettingsManager.SaveSettings(_settings);
        }

        /// <summary>
        /// Handles the selected tab changed event of the ribbon.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void RibbonBarSelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            if (e.RibbonTabVM == null)
            {
                ContentArea = null;
            }
            else if (e.RibbonTabVM.Header == RibbonTabName.CATEGORIES)
            {
                ContentArea = _composer.GetExportedValue<ICategoriesVM>();
            }
            else if (e.RibbonTabVM.Header == RibbonTabName.DOWNLOADS)
            {
                ContentArea = _composer.GetExportedValue<IDownloadsVM>();
            }
        }

        #endregion Private Methods
    }
}