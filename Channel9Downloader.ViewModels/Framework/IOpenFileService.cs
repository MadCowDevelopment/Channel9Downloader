namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface defines a interface that will allow 
    /// a ViewModel to open a file
    /// </summary>
    public interface IOpenFileService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        string FileName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        string Filter
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the initial directory.
        /// </summary>
        string InitialDirectory
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// This method should show a window that allows a file to be selected
        /// </summary>
        /// <returns>A bool from the ShowDialog call</returns>
        bool? ShowDialog();

        #endregion Methods
    }
}