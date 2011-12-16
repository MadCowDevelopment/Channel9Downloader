using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a series.
    /// </summary>
    [DataContract]
    public class Series : RecurringCategory
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Series"/> class.
        /// Copy constructor for <see cref="RecurringCategory"/>.
        /// </summary>
        /// <param name="recurringCategory">The recurring category whose values should be copied.</param>
        public Series(RecurringCategory recurringCategory)
        {
            Description = recurringCategory.Description;
            RelativePath = recurringCategory.RelativePath;
            Title = recurringCategory.Title;
        }

        #endregion Constructors
    }
}