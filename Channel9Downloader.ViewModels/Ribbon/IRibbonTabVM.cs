using System.Collections.ObjectModel;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface provides access to ribbon tab items.
    /// </summary>
    public interface IRibbonTabVM : IObservableObject
    {
        #region Properties

        /// <summary>
        /// Gets the groups of this tab.
        /// </summary>
        ObservableCollection<IRibbonGroupVM> Groups
        {
            get;
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        string Header
        {
            get; set;
        }

        #endregion Properties
    }
}