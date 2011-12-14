namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a show.
    /// </summary>
    public class Show : RecurringCategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Show"/> class.
        /// Copy constructor for <see cref="RecurringCategory"/>.
        /// </summary>
        /// <param name="recurringCategory">The recurring category whose values should be copied.</param>
        public Show(RecurringCategory recurringCategory)
        {
            Description = recurringCategory.Description;
            RelativePath = recurringCategory.RelativePath;
            Title = recurringCategory.Title;
        }
    }
}
