using System;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Events
{
    /// <summary>
    /// This class contains information for a dialog request.
    /// </summary>
    public class DialogRequestEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogRequestEventArgs"/> class.
        /// </summary>
        /// <param name="viewModel">The viewmodel that will be used as DataContext for the view.</param>
        public DialogRequestEventArgs(ISimpleViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the viewmodel for the dialog request.
        /// </summary>
        public ISimpleViewModel ViewModel
        {
            get; private set;
        }

        #endregion Public Properties
    }
}