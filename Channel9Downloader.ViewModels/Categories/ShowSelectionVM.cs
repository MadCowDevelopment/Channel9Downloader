using System.ComponentModel.Composition;

using Channel9Downloader.Entities;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the show selection view.
    /// </summary>
    [Export(typeof(IShowSelectionVM))]
    public class ShowSelectionVM : CategorySelectionVM<Show>, IShowSelectionVM
    {
    }
}