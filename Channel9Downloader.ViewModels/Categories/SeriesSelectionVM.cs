using System;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the series selection view.
    /// </summary>
    [Export(typeof(ISeriesSelectionVM))]
    public class SeriesSelectionVM : BaseViewModel, ISeriesSelectionVM
    {
        #region Fields

        /// <summary>
        /// The category browser.
        /// </summary>
        private readonly IChannel9CategoryBrowser _categoryBrowser;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesSelectionVM"/> class.
        /// </summary>
        /// <param name="categoryBrowser">The category browser.</param>
        [ImportingConstructor]
        public SeriesSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            _categoryBrowser = categoryBrowser;
        }

        #endregion Constructors

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
        public void Initialize()
        {
            var series = _categoryBrowser.GetAllSeries();
            SeriesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(series);
        }

        #endregion Public Methods
    }
}