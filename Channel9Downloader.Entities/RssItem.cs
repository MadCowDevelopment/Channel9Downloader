using System;
using System.Collections.Generic;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about an RSS item.
    /// </summary>
    public class RssItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RssItem"/> class.
        /// </summary>
        public RssItem()
        {
            MediaGroup = new List<MediaContent>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Guid.
        /// </summary>
        public string Guid
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a list of media content.
        /// </summary>
        public List<MediaContent> MediaGroup
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the publishing date.
        /// </summary>
        public DateTime PubDate
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a summary.
        /// </summary>
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get; set;
        }

        #endregion Public Properties
    }
}