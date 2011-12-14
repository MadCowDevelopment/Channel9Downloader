using System.ComponentModel.Composition;

using Microsoft.Win32;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class implements the <see cref="ISaveFileService"/> for WPF purposes.
    /// </summary>
    [Export(typeof(ISaveFileService))]
    public class WpfSaveFileService : ISaveFileService
    {
        #region Fields

        /// <summary>
        /// Embedded <see cref="SaveFileDialog"/> to pass back correctly selected
        /// values to ViewModel
        /// </summary>
        private readonly SaveFileDialog _sfd = new SaveFileDialog();

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the selected filename.
        /// </summary>
        public string FileName
        {
            get
            {
                return _sfd.FileName;
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
            get { return _sfd.Filter; }
            set { _sfd.Filter = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory.
        /// </summary>
        public string InitialDirectory
        {
            get { return _sfd.InitialDirectory; }
            set { _sfd.InitialDirectory = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether prompt should be overwritten.
        /// </summary>
        public bool OverwritePrompt
        {
            get { return _sfd.OverwritePrompt; }
            set { _sfd.OverwritePrompt = value; }
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
                _sfd.Filter = Filter;
            }

            if (!string.IsNullOrEmpty(InitialDirectory))
            {
                _sfd.InitialDirectory = InitialDirectory;
            }

            _sfd.OverwritePrompt = OverwritePrompt;

            return _sfd.ShowDialog();
        }

        #endregion Public Methods
    }
}