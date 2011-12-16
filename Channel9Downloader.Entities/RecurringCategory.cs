using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a recurring category, e.g. Show, Series,..
    /// </summary>
    [DataContract]
    public class RecurringCategory : Category
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember]
        public string Description
        {
            get; set;
        }

        #endregion Public Properties
    }
}