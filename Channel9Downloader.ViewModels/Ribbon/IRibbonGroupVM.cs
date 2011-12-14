using System.Collections.ObjectModel;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface provides access to a ribbon group.
    /// </summary>
    public interface IRibbonGroupVM : IObservableObject
    {
        /// <summary>
        /// Gets or sets the group's header.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Gets the group's items.
        /// </summary>
        ObservableCollection<IRibbonItemVM> Items { get; }
    }
}
