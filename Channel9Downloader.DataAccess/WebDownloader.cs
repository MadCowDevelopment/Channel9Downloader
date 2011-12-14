using System.ComponentModel.Composition;
using System.Net;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class encapsulates the functionality of <see cref="WebClient"/>.
    /// </summary>
    [Export(typeof(IWebDownloader))]
    public class WebDownloader : IWebDownloader
    {
        #region Public Methods

        /// <summary>
        /// Downloads the requested resource as a String. 
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a String.</returns>
        public string DownloadString(string address)
        {
            var webClient = new WebClient();
            var result = webClient.DownloadString(address);
            return result;
        }

        #endregion Public Methods
    }
}