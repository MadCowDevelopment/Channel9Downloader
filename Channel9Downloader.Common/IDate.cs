using System;

namespace Channel9Downloader.Common
{
    /// <summary>
    /// This interface provides methods for date and time.
    /// </summary>
    public interface IDate
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        DateTime Now { get; }
    }
}
