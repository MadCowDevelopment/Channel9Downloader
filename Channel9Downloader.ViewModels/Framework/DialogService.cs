using System.ComponentModel.Composition;
using System.Windows;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class shows a dialog.
    /// </summary>
    [Export(typeof(IDialogService))]
    public class DialogService : IDialogService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        public DialogService()
        {
            ResizeMode = ResizeMode.CanResize;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the resize mode. 
        /// </summary>
        public ResizeMode ResizeMode
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <param name="title">Title of the dialog.</param>
        /// <param name="dataContext">DataContext of the dialog.</param>
        /// <returns>Returns the dialog result.</returns>
        public bool? ShowDialog(string title, object dataContext)
        {
            var win = new WindowDialog();
            win.Title = title;
            win.DataContext = dataContext;
            win.ResizeMode = ResizeMode;
            return win.ShowDialog();
        }

        #endregion Public Methods
    }
}