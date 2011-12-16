using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class contains all categories.
    /// </summary>
    [DataContract]
    public class Categories
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Categories"/> class.
        /// </summary>
        /// <param name="tags">List of all tags.</param>
        /// <param name="shows">List of all shows.</param>
        /// <param name="series">List of all series.</param>
        public Categories(IEnumerable<Tag> tags, IEnumerable<Show> shows, IEnumerable<Series> series)
        {
            Tags = new List<Tag>(tags);
            Shows = new List<Show>(shows);
            Series = new List<Series>(series);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a list of all series.
        /// </summary>
        [DataMember]
        public List<Series> Series
        {
            get; private set;
        }

        /// <summary>
        /// Gets a list of all shows.
        /// </summary>
        [DataMember]
        public List<Show> Shows
        {
            get; private set;
        }

        /// <summary>
        /// Gets a list of all tags.
        /// </summary>
        [DataMember]
        public List<Tag> Tags
        {
            get; private set;
        }

        #endregion Public Properties
    }
}