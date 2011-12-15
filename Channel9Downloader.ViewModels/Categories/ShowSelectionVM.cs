using System.ComponentModel.Composition;
using System.Windows.Data;
using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    [Export(typeof(IShowSelectionVM))]
    public class ShowSelectionVM : ViewModelBase, IShowSelectionVM
    {
        [ImportingConstructor]
        public ShowSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            var shows = categoryBrowser.GetAllShows();
            ShowCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(shows);
        }

        public CollectionView ShowCollectionView { get; private set; }
    }
}
