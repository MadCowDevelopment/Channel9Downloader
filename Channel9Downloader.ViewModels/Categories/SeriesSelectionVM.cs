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
    public class SeriesSelectionVM : ViewModelBase, ISeriesSelectionVM
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesSelectionVM"/> class.
        /// </summary>
        /// <param name="categoryBrowser">The category browser.</param>
        [ImportingConstructor]
        public SeriesSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            var series = categoryBrowser.GetAllSeries();
            SeriesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(series);
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
    }
}