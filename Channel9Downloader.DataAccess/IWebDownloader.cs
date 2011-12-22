using System;
using System.ComponentModel;
using System.Net;
using System.Xml.Linq;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface contains methods to download strings.
    /// </summary>
    public interface IWebDownloader
    {
        #region Events

        /// <summary>
        /// Occurs when an asynchronous file download operation completes.
        /// </summary>
        event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted;

        /// <summary>
        /// Occurs when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        #endregion Events

        #region Methods

        /// <summary>
        /// Cancels a pending asynchronous operation.
        /// </summary>
        void CancelAsync();

        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="address">The URI from which to download data.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        void DownloadData(string address, string filename);

        /// <summary>
        /// Downloads a file asynchronously.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        void DownloadFileAsync(Uri address, string filename);

        /// <summary>
        /// Downloads the requested resource as a String. 
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a String.</returns>
        string DownloadString(string address);

        /// <summary>
        /// Downloads the requested resource as XHTML.
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a XDocument.</returns>
        XDocument DownloadXHTML(string address);

        #endregion Methods
    }
}