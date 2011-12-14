using System.Windows.Forms;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface allows opening of a folder browse dialog.
    /// </summary>
    public interface IOpenFolderService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        string SelectedPath
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the new folder button should be shown.
        /// </summary>
        bool ShowNewFolderButton
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <returns>Returns the result of the dialog.</returns>
        DialogResult ShowDialog();

        #endregion Methods
    }
}