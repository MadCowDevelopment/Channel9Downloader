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
        protected IViewModelBase _contentArea;

        [ImportingConstructor]
        public MainWindowVM(IRibbonVM ribbon)
        {
            RibbonBar = ribbon;
            RibbonBar.SelectedTabChanged += RibbonBarSelectedTabChanged;
        }

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

        #region Public Methods

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

        #endregion Public Methods
    }
}