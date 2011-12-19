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
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDownloader"/> class.
        /// </summary>
        public WebDownloader()
        {
            ServicePointManager.DefaultConnectionLimit = 16;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="address">The URI from which to download data.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        public void DownloadData(string address, string filename)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(address, filename);
        }

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

        #endregion Private Methods
    }
}