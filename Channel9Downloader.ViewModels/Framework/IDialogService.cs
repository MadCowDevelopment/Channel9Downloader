using System.Windows;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface is used to show a dialog.
    /// </summary>
    public interface IDialogService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the resize mode.
        /// </summary>
        ResizeMode ResizeMode
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <param name="title">Title of the dialog.</param>
        /// <param name="dataContext">DataContext of the dialog.</param>
        /// <returns>Returns the dialog result.</returns>
        bool? ShowDialog(string title, object dataContext);

        #endregion Methods
    }
}