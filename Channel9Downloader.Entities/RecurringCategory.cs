namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a recurring category, e.g. Show, Series,..
    /// </summary>
    public class RecurringCategory : Category
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get; set;
        }

        #endregion Public Properties
    }
}