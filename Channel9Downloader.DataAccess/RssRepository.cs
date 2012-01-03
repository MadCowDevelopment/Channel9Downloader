using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using Channel9Downloader.Composition;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class provides access to RSS feed items.
    /// </summary>
    [Export(typeof(IRssRepository))]
    public class RssRepository : IRssRepository
    {
        private readonly IDependencyComposer _dependencyComposer;

        #region Fields

        /// <summary>
        /// Namespace for ITunes elements.
        /// </summary>
        private readonly XNamespace _itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";

        /// <summary>
        /// Namespace for media elements.
        /// </summary>
        private readonly XNamespace _media = "http://search.yahoo.com/mrss/";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RssRepository"/> class.
        /// </summary>
        /// <param name="dependencyComposer">The composer used for retrieving objects.</param>
        [ImportingConstructor]
        public RssRepository(IDependencyComposer dependencyComposer)
        {
            _dependencyComposer = dependencyComposer;
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
            var items = ParseRssItems(doc);
            return items;
        }

        /// <summary>
        /// Gets all RSS items.
        /// </summary>
        /// <returns>Returns a list of all RSS items.</returns>
        public List<RssItem> GetRssItems()
        {
            var doc = DownloadFeedData();
            var items = ParseRssItems(doc);
            return items;
        }

        /// <summary>
        /// Parses RSS feed data.
        /// </summary>
        /// <param name="doc">The document to parse.</param>
        /// <returns>Returns a list of <see cref="RssItem"/>.</returns>
        private List<RssItem> ParseRssItems(XDocument doc)
        {
            var items = from item in doc.Element("rss").Element("channel").Elements("item")
                        select
                            new RssItem
                                {
                                    Description = HttpUtility.HtmlDecode(item.Element("title").Value),
                                    Guid = item.Element("guid").Value,
                                    MediaGroup = (from content in item.Element(_media + "group").Elements(_media + "content")
                                                  select
                                                      new MediaContent
                                                          {
                                                              FileSize = int.Parse(content.Attribute("fileSize").Value),
                                                              Medium = content.Attribute("medium").Value,
                                                              Type = content.Attribute("type").Value,
                                                              Url = content.Attribute("url").Value
                                                          }).ToList(),
                                    PubDate = DateTime.Parse(item.Element("pubDate").Value),
                                    Summary = HttpUtility.HtmlDecode(item.Element(_itunes + "summary").Value),
                                    Title = HttpUtility.HtmlDecode(item.Element("title").Value),
                                    Thumbnails = (from thumbnail in item.Elements(_media + "thumbnail")
                                                  select
                                                      new Thumbnail
                                                          {
                                                              Height = int.Parse(thumbnail.Attribute("height").Value),
                                                              Url = thumbnail.Attribute("url").Value,
                                                              Width = int.Parse(thumbnail.Attribute("width").Value)
                                                          }).ToList()
                                };

            return items.Where(p => p.MediaGroup.Any(m => m.Medium == "video" && m.Type == "video/x-ms-wmv")).ToList();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Downloads RSS feed data for the specified category.
        /// </summary>
        /// <param name="category">Category of the feed.</param>
        /// <returns>Returns the feed data.</returns>
        private XDocument DownloadFeedData(Category category)
        {
            return DownloadFeedData(string.Format("http://channel9.msdn.com{0}/RSS", category.RelativePath));
        }

        /// <summary>
        /// Downloads RSS feed data.
        /// </summary>
        /// <returns>Returns the feed data.</returns>
        private XDocument DownloadFeedData()
        {
            return DownloadFeedData(string.Format("http://channel9.msdn.com/Feeds/RSS"));
        }

        /// <summary>
        /// Downloads RSS feed data from the specified address.
        /// </summary>
        /// <param name="address">The address of the RSS feed.</param>
        /// <returns>Returns the feed data.</returns>
        private XDocument DownloadFeedData(string address)
        {
            var webDownloader = _dependencyComposer.GetExportedValue<IWebDownloader>();
            var rssFeedData = webDownloader.DownloadString(address);
            var doc = XDocument.Parse(rssFeedData);
            return doc;
        }

        #endregion Private Methods
    }
}