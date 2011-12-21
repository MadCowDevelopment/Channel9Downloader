using System.ComponentModel.Composition;

using Channel9Downloader.Entities;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the series selection view.
    /// </summary>
    [Export(typeof(ISeriesSelectionVM))]
    public class SeriesSelectionVM : CategorySelectionVM<Series>, ISeriesSelectionVM
    {
    }
}