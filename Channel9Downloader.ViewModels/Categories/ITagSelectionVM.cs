using System.Windows.Data;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    public interface ITagSelectionVM : IViewModelBase
    {
        CollectionView TagsCollectionView { get; }
    }
}
