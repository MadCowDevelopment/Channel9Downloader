using System.ComponentModel.Composition;
using System.Windows.Data;
using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    [Export(typeof(ISeriesSelectionVM))]
    public class SeriesSelectionVM : ViewModelBase, ISeriesSelectionVM
    {
        [ImportingConstructor]
        public SeriesSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            var series = categoryBrowser.GetAllSeries();
            SeriesCollectionView = (CollectionView) CollectionViewSource.GetDefaultView(series);
        }

        public CollectionView SeriesCollectionView { get; private set; }
    }
}
