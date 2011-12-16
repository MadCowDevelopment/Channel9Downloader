using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the series selection view.
    /// </summary>
    [Export(typeof(ISeriesSelectionVM))]
    public class SeriesSelectionVM : BaseViewModel, ISeriesSelectionVM
    {
        #region Public Properties

        /// <summary>
        /// Gets a collection of series.
        /// </summary>
        public CollectionView SeriesCollectionView
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="series">The series which are used to initialize this viewmodel.
        /// </param>
        public void Initialize(List<Series> series)
        {
            SeriesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(series);
        }

        #endregion Public Methods
    }
}