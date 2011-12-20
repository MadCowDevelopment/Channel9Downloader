namespace Channel9Downloader.Entities
{
    #region Enumerations

    /// <summary>
    /// This enum contains the different states a download can be in.
    /// </summary>
    public enum DownloadState
    {
        /// <summary>
        /// The download is downloading.
        /// </summary>
        Downloading,

        /// <summary>
        /// The download is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The download has finished.
        /// </summary>
        Finished,

        /// <summary>
        /// The download is ignored.
        /// </summary>
        Ignored
    }

    #endregion Enumerations
}