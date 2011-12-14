using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class provides a folder browse dialog to viewmodels.
    /// </summary>
    [Export(typeof(IOpenFolderService))]
    public class WpfOpenFolderService : IOpenFolderService, IDisposable
    {
        #region Fields

        /// <summary>
        /// The folder browse dialog that will be shown.
        /// </summary>
        private readonly FolderBrowserDialog _fbd = new FolderBrowserDialog();

        /// <summary>
        /// Determines whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get
            {
                return _fbd.Description;
            }

            set
            {
                _fbd.Description = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        public string SelectedPath
        {
            get
            {
                return _fbd.SelectedPath;
            }

            set
            {
                _fbd.SelectedPath = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a new folder button should be shown.
        /// </summary>
        public bool ShowNewFolderButton
        {
            get
            {
                return _fbd.ShowNewFolderButton;
            }

            set
            {
                _fbd.ShowNewFolderButton = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Shows the folder browse dialog.
        /// </summary>
        /// <returns>Returns the result of the dialog.</returns>
        public DialogResult ShowDialog()
        {
            return _fbd.ShowDialog();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Disposes managed and unmanaged resources. 
        /// </summary>
        /// <param name="disposing">Determines whether dispose has been called from user code (<see langword="true"/>) 
        /// or from a finalizer (<see langword="false"/>).</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fbd.Dispose();
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            _disposed = true;
        }

        #endregion Protected Methods
    }
}