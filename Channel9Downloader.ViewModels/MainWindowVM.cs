using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Channel9Downloader.Composition;
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

        public void Initialize()
        {
            RibbonBar.Initialize();
            RibbonBar.SelectedTabChanged += RibbonBarSelectedTabChanged;
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

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Handles the selected tab changed event of the ribbon.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void RibbonBarSelectedTabChanged(object sender, Events.SelectedTabChangedEventArgs e)
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

        private ICommand _showSettingsViewCommand;

        public ICommand ShowSettingsViewCommand
        {
            get 
            {
                return _showSettingsViewCommand ??
                       (_showSettingsViewCommand = new RelayCommand(p => OnShowSettingsView()));
            }
        }

        private void OnShowSettingsView()
        {
            var settingsVM = _composer.GetExportedValue<ISettingsVM>();
            OnDialogRequested(new ShowDialogEventArgs(settingsVM));
        }

        public event EventHandler<ShowDialogEventArgs> DialogRequested;

        public void OnDialogRequested(ShowDialogEventArgs e)
        {
            var handler = DialogRequested;
            if (handler != null) handler(this, e);
        }
    }
}