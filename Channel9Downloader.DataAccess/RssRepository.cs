using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class provides access to RSS feed items.
    /// </summary>
    [Export(typeof(IRssRepository))]
    public class RssRepository : IRssRepository
    {
        #region Fields

        /// <summary>
        /// Namespace for ITunes elements.
        /// </summary>
        private readonly XNamespace _itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

        /// <summary>
        /// Namespace for media elements.
        /// </summary>
        private readonly XNamespace _media = "http://search.yahoo.com/mrss/";

        /// <summary>
        /// The downloader used for retrieving web files.
        /// </summary>
        private readonly IWebDownloader _webDownloader;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RssRepository"/> class.
        /// </summary>
        /// <param name="webDownloader">The downloader used for retrieving web files.</param>
        [ImportingConstructor]
        public RssRepository(IWebDownloader webDownloader)
        {
            _webDownloader = webDownloader;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Gets all RSS items for the specified category.
        /// </summary>
        /// <param name="category">The category for which RSS items should be retrieved.</param>
        /// <returns>Returns a list of all RSS items of the specified category.</returns>
        public List<RssItem> GetRssItems(Category category)
        {
            var doc = DownloadFeedData(category);

            var items = from item in doc.Element("rss").Element("channel").Elements("item")
                        select
                            new RssItem
                                {
                                    Description = item.Element("title").Value,
                                    Guid = item.Element("guid").Value,
                                    MediaGroup = (from content in item.Element(_media + "group").Elements(_media + "content")
                                                 select new MediaContent
                                                     {
                                                         FileSize = int.Parse(content.Attribute("fileSize").Value),
                                                         Medium = content.Attribute("medium").Value,
                                                         Type = content.Attribute("type").Value,
                                                         Url = content.Attribute("url").Value
                                                     }).ToList(),
                                    PubDate = DateTime.Parse(item.Element("pubDate").Value),
                                    Summary = item.Element(_itunes + "summary").Value,
                                    Title = item.Element("title").Value
                                };

            return items.Where(p => p.MediaGroup.Any(m => m.Medium == "video")).ToList();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Downloads the RSS feed data.
        /// </summary>
        /// <param name="category">Category of the feed.</param>
        /// <returns>Returns the feed data.</returns>
        private XDocument DownloadFeedData(Category category)
        {
            var url = string.Format("http://channel9.msdn.com{0}/RSS", category.RelativePath);
            var rssFeedData = _webDownloader.DownloadString(url);
            var doc = XDocument.Parse(rssFeedData);
            return doc;
        }

        #endregion Private Methods
    }
}