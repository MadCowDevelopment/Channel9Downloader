namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface is used to show dialogs from within a viewmodel.
    /// </summary>
    public interface IViewService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data context of the view.
        /// </summary>
        object DataContext
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Closes the window.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows the window.
        /// </summary>
        void Show();

        /// <summary>
        /// Shows the window as modal dialog.
        /// </summary>
        /// <returns>Returns a dialog result.</returns>
        bool? ShowDialog();

        #endregion Methods
    }
}