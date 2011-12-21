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
        /// The download has been stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// The download is queued.
        /// </summary>
        Queued,

        /// <summary>
        /// The download has finished.
        /// </summary>
        Finished,

        /// <summary>
        /// The download encountered an error.
        /// </summary>
        Error,

        /// <summary>
        /// The download is skipped.
        /// </summary>
        Skipped
    }

    #endregion Enumerations
}