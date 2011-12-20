using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class contains finished downloads.
    /// </summary>
    [DataContract]
    public class FinishedDownloads
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FinishedDownloads"/> class.
        /// </summary>
        public FinishedDownloads()
        {
            Items = new List<RssItem>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a list of finished downloads.
        /// </summary>
        [DataMember]
        public List<RssItem> Items
        {
            get; set;
        }

        #endregion Public Properties
    }
}