using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class provides logic and binding for the main window.
    /// </summary>
    [Export(typeof(IMainWindowVM))]
    public class MainWindowVM : SimpleViewModel, IMainWindowVM
    {
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
        /// Gets the viewmodel for the main content.
        /// </summary>
        [Import]
        public IContentAreaVM ContentArea { get; private set; }
    }
}
