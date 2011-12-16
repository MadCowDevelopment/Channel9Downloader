using System.Windows.Data;

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
        /// Initializes the viewmodel.
        /// </summary>
        void Initialize();

        #endregion Methods
    }
}