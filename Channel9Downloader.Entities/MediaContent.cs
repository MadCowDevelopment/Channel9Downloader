namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about media content.
    /// </summary>
    public class MediaContent
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        public int FileSize
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        public string Medium
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url
        {
            get; set;
        }

        #endregion Public Properties
    }
}