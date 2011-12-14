using System.Collections.ObjectModel;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class manages a ribbon tab.
    /// </summary>
    public class RibbonTabVM : ObservableObject, IRibbonTabVM
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonTabVM"/> class.
        /// </summary>
        public RibbonTabVM()
        {
            Groups = new ObservableCollection<IRibbonGroupVM>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets all groups of this tab.
        /// </summary>
        public ObservableCollection<IRibbonGroupVM> Groups
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets header.
        /// </summary>
        public string Header
        {
            get; set;
        }

        #endregion Public Properties
    }
}