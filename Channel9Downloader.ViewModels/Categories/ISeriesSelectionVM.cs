using System.Collections.Generic;
using System.Windows.Data;

using Channel9Downloader.Entities;
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
        /// Initializes this view.
        /// </summary>
        /// <param name="series">The series which are used to initialize this viewmodel.
        /// </param>
        void Initialize(List<Series> series);

        #endregion Methods
    }
}