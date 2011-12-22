using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class serves as base class for different video categories.
    /// </summary>
    [DataContract]
    public class Category : ObservableModel
    {
        #region Fields

        /// <summary>
        /// Name of <see cref="IsEnabled"/> property.
        /// </summary>
        public const string PROP_IS_ENABLED = "IsEnabled";

        /// <summary>
        /// Backing field for <see cref="IsEnabled"/> property.
        /// </summary>
        private bool _isEnabled;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the categories description.
        /// </summary>
        [DataMember]
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this category is enabled.
        /// </summary>
        [DataMember]
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
                RaisePropertyChanged(PROP_IS_ENABLED);
            }
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