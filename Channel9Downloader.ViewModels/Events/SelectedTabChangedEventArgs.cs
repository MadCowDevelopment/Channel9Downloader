using System;

using Channel9Downloader.ViewModels.Ribbon;

namespace Channel9Downloader.ViewModels.Events
{
    /// <summary>
    /// This class holds event args for the event when the selected ribbon tab changes.
    /// </summary>
    public class SelectedTabChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedTabChangedEventArgs"/> class.
        /// </summary>
        /// <param name="ribbonTabVM">The selected ribbon tab.</param>
        public SelectedTabChangedEventArgs(IRibbonTabVM ribbonTabVM)
        {
            RibbonTabVM = ribbonTabVM;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the selected ribbon tab.
        /// </summary>
        public IRibbonTabVM RibbonTabVM
        {
            get; set;
        }

        #endregion Public Properties
    }
}