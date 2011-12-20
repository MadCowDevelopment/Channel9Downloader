using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a thumbnail.
    /// </summary>
    [DataContract]
    public class Thumbnail
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [DataMember]
        public int Height
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [DataMember]
        public string Url
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [DataMember]
        public int Width
        {
            get; set;
        }

        #endregion Public Properties
    }
}