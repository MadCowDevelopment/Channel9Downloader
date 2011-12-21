using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a series.
    /// </summary>
    [DataContract]
    public class Series : Category
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Series"/> class.
        /// Copy constructor for <see cref="Category"/>.
        /// </summary>
        /// <param name="category">The recurring category whose values should be copied.</param>
        public Series(Category category)
        {
            Description = category.Description;
            RelativePath = category.RelativePath;
            Title = category.Title;
        }

        #endregion Constructors
    }
}