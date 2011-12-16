using System.Windows.Data;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This interface is used for the series selection viewmodel.
    /// </summary>
    public interface ISeriesSelectionVM : IBaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets a collection of series.
        /// </summary>
        CollectionView SeriesCollectionView
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