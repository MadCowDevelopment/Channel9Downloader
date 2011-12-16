using System.Collections.Generic;
using System.Windows.Data;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This interface is used for the tag selection viewmodel.
    /// </summary>
    public interface ITagSelectionVM : IBaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets a collection view of tags that the view can bind to.
        /// </summary>
        CollectionView TagsCollectionView
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="tags">The tags which are used to initialize this viewmodel.
        /// </param>
        void Initialize(List<Tag> tags);

        #endregion Methods
    }
}