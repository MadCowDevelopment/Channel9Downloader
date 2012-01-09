using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class encapsulates the functionality of <see cref="WebClient"/>.
    /// </summary>
    [Export(typeof(IWebDownloader))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WebDownloader : IWebDownloader
    {
        #region Fields

        /// <summary>
        /// The underlying webclient.
        /// </summary>
        private readonly WebClient _webClient;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDownloader"/> class.
        /// </summary>
        public WebDownloader()
        {
            ServicePointManager.DefaultConnectionLimit = 16;
            _webClient = new WebClient();
            _webClient.DownloadFileCompleted += (sender, args) => RaiseDownloadFileCompleted(args);
            _webClient.DownloadProgressChanged += (sender, args) => RaiseDownloadProgressChanged(args);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when an asynchronous file download operation completes.
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted;

        /// <summary>
        /// Occurs when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        #endregion Events

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether a web request is in progress.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _webClient.IsBusy;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Cancels a pending asynchronous operation.
        /// </summary>
        public void CancelAsync()
        {
            _webClient.CancelAsync();
        }

        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="address">The URI from which to download data.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        public void DownloadData(string address, string filename)
        {
            _webClient.DownloadFile(new Uri(address), filename);
        }

        /// <summary>
        /// Downloads a file asynchronously.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        public void DownloadFileAsync(Uri address, string filename)
        {
            _webClient.DownloadFileAsync(address, filename);
        }

        /// <summary>
        /// Downloads the requested resource as a String. 
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a String.</returns>
        public string DownloadString(string address)
        {
            var result = _webClient.DownloadString(address);
            return result;
        }

        /// <summary>
        /// Downloads the requested resource as XHTML.
        /// The resource to download is specified as a String containing the URI.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>Returns the downloaded resource as a <see cref="XDocument"/>.</returns>
        public XDocument DownloadXHTML(string address)
        {
            var downloadString = DownloadString(address);
            var textReader = new StringReader(downloadString);
            var doc = FromHtml(textReader);
            return doc;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Returns an <see cref="XDocument"/> from <see cref="TextReader"/> that contains HTML.
        /// </summary>
        /// <param name="reader">The reader used for getting HTML.</param>
        /// <returns>Returns an XML representation of the HTML.</returns>
        private XDocument FromHtml(TextReader reader)
        {
            var sgmlReader = new Sgml.SgmlReader();
            sgmlReader.DocType = "HTML";
            sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
            sgmlReader.InputStream = reader;

            var doc = XDocument.Load(sgmlReader);
            return doc;
        }

        /// <summary>
        /// Raises the <see cref="DownloadFileCompleted"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            var handler = DownloadFileCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadProgressChanged"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadProgressChanged(DownloadProgressChangedEventArgs e)
        {
            var handler = DownloadProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Private Methods
    }
}