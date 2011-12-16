using System.Collections.Generic;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface provides methods to create ribbon elements.
    /// </summary>
    public interface IRibbonFactory
    {
        #region Methods

        /// <summary>
        /// Creates the ribbon tabs.
        /// </summary>
        /// <returns>Returns a list of ribbon tabs.</returns>
        List<IRibbonTabVM> CreateRibbonTabs();

        #endregion Methods
    }
}