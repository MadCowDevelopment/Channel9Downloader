using System.Collections.ObjectModel;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class manages a ribbon group.
    /// </summary>
    public class RibbonGroupVM : ObservableObject, IRibbonGroupVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonGroupVM"/> class.
        /// </summary>
        public RibbonGroupVM()
        {
            Items = new ObservableCollection<IRibbonItemVM>();
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets the items in this group.
        /// </summary>
        public ObservableCollection<IRibbonItemVM> Items { get; private set; }
    }
}
