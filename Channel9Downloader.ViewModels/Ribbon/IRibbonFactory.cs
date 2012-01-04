using System.Collections.Generic;

using Channel9Downloader.ViewModels.Categories;

namespace Channel9Downloader.ViewModels.Ribbon
{
    using Channel9Downloader.ViewModels.Dashboard;

    /// <summary>
    /// This interface provides methods to create ribbon elements.
    /// </summary>
    public interface IRibbonFactory
    {
        #region Properties

        /// <summary>
        /// Gets or sets the categories viewmodel.
        /// </summary>
        ICategoriesVM CategoriesVM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the main window viewmodel.
        /// </summary>
        IMainWindowVM MainWindowVM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the downloads viewmodel.
        /// </summary>
        IDownloadsVM DownloadsVM
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dashboard viewmodel.
        /// </summary>
        IDashboardVM DashboardVM { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the ribbon tabs.
        /// </summary>
        /// <returns>Returns a list of ribbon tabs.</returns>
        List<IRibbonTabVM> CreateRibbonTabs();

        #endregion Methods
    }
}