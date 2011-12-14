namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface contains methods to download strings.
    /// </summary>
    public interface IWebDownloader
    {
        #region Methods

        /// <summary>
        /// Downloads the requested resource as a String. 
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a String.</returns>
        string DownloadString(string address);

        #endregion Methods
    }
}