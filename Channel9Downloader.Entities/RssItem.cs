using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about an RSS item.
    /// </summary>
    [DataContract]
    public class RssItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RssItem"/> class.
        /// </summary>
        public RssItem()
        {
            MediaGroup = new List<MediaContent>();
            Thumbnails = new List<Thumbnail>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember]
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Guid.
        /// </summary>
        [DataMember]
        public string Guid
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a list of media content.
        /// </summary>
        [DataMember]
        public List<MediaContent> MediaGroup
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the publishing date.
        /// </summary>
        [DataMember]
        public DateTime PubDate
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a summary.
        /// </summary>
        [DataMember]
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a list of thumbnails.
        /// </summary>
        [DataMember]
        public List<Thumbnail> Thumbnails
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember]
        public string Title
        {
            get; set;
        }

        #endregion Public Properties
    }
}