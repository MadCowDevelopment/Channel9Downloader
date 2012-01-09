namespace Channel9Downloader.Entities
{
    #region Enumerations

    /// <summary>
    /// Enumeration for the different download priorities.
    /// </summary>
    public enum DownloadPriority
    {
        /// <summary>
        /// This indicates that downloads will be preferred.
        /// </summary>
        High,

        /// <summary>
        /// This indicates the normal priority.
        /// </summary>
        Normal,

        /// <summary>
        /// This indicates that downloads will be deferred.
        /// </summary>
        Low
    }

    #endregion Enumerations
}