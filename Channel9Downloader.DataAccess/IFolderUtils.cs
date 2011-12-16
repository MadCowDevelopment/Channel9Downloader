namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for retrieving special folders.
    /// </summary>
    public interface IFolderUtils
    {
        #region Methods

        /// <summary>
        /// Gets the user's application data path.
        /// </summary>
        /// <returns>Returns the user's application data path.</returns>
        string GetUserDataPath();

        #endregion Methods
    }
}