using System.ComponentModel.Composition;

using Microsoft.Win32;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class implements the <see cref="IOpenFileService"/> for WPF purposes.
    /// </summary>
    [Export(typeof(IOpenFileService))]
    public class WpfOpenFileService : IOpenFileService
    {
        #region Fields

        /// <summary>
        /// Embedded <see cref="OpenFileDialog"/> to pass back correctly selected
        /// values to ViewModel
        /// </summary>
        private readonly OpenFileDialog _ofd = new OpenFileDialog();

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string FileName
        {
            get
            {
                return _ofd.FileName;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter
        {
            get { return _ofd.Filter; }
            set { _ofd.Filter = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory.
        /// </summary>
        public string InitialDirectory
        {
            get { return _ofd.InitialDirectory; }
            set { _ofd.InitialDirectory = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// This method should show a window that allows a file to be selected
        /// </summary>
        /// <returns>A bool from the ShowDialog call</returns>
        public bool? ShowDialog()
        {
            if (!string.IsNullOrEmpty(Filter))
            {
                _ofd.Filter = Filter;
            }

            if (!string.IsNullOrEmpty(InitialDirectory))
            {
                _ofd.InitialDirectory = InitialDirectory;
            }

            return _ofd.ShowDialog();
        }

        #endregion Public Methods
    }
}