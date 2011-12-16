using System.Windows.Data;

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
        CollectionView ShowCollectionView
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