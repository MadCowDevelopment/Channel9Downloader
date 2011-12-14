using System;
using System.Collections.ObjectModel;

using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used for the ribbon bar viewmodel.
    /// </summary>
    public interface IRibbonVM : IObservableObject
    {
        #region Events

        /// <summary>
        /// This event is raised when the selected ribbon tab changed.
        /// </summary>
        event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the selected tab.
        /// </summary>
        IRibbonTabVM SelectedTab
        {
            get; set;
        }

        /// <summary>
        /// Gets the ribbon's tabs.
        /// </summary>
        ObservableCollection<IRibbonTabVM> Tabs
        {
            get;
        }

        #endregion Properties
    }
}