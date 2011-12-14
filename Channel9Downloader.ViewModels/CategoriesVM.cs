using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class manages the categories view.
    /// </summary>
    [Export(typeof(ICategoriesVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CategoriesVM : ViewModelBase, ICategoriesVM
    {
    }
}