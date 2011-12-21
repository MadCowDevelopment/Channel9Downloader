using System.Collections.Generic;
using System.Windows.Data;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This interface is used for the show selection VM.
    /// </summary>
    public interface IShowSelectionVM : IBaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets a collection of shows.
        /// </summary>
        CollectionView CategoriesCollectionView
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="shows">The shows which are used to initialize this viewmodel.
        /// </param>
        void Initialize(List<Show> shows);

        #endregion Methods
    }
}