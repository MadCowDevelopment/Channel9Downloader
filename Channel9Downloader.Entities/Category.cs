using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class serves as base class for different video categories.
    /// </summary>
    [DataContract]
    public abstract class Category
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this category is enabled.
        /// </summary>
        [DataMember]
        public bool IsEnabled
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the relative path for the category.
        /// </summary>
        [DataMember]
        public string RelativePath
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the title of the category.
        /// </summary>
        [DataMember]
        public string Title
        {
            get; set;
        }

        #endregion Public Properties
    }
}