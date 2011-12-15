using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    public interface ISeriesSelectionVM : IViewModelBase
    {
        CollectionView SeriesCollectionView { get; }
    }
}
