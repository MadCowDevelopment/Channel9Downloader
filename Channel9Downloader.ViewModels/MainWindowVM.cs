using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;
using Channel9Downloader.ViewModels.Ribbon;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class provides logic and binding for the main window.
    /// </summary>
    [Export(typeof(IMainWindowVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainWindowVM : SimpleViewModel, IMainWindowVM
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="ContentArea"/> property.
        /// </summary>
        private IViewModelBase _contentArea;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVM"/> class.
        /// </summary>
        /// <param name="ribbon">The ribbon that is used.</param>
        [ImportingConstructor]
        public MainWindowVM(IRibbonVM ribbon)
        {
            RibbonBar = ribbon;
            RibbonBar.SelectedTabChanged += RibbonBarSelectedTabChanged;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the viewmodel for the main content.
        /// </summary>
        public IViewModelBase ContentArea
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
                ContentArea = new CategoriesVM();
            }
            else if (e.RibbonTabVM.Header == RibbonTabName.DOWNLOADS)
            {
                ContentArea = new DownloadsVM();
            }
        }

        #endregion Private Methods
    }
}