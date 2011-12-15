using System.Windows.Input;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This interface is for the categories viewmodel.
    /// </summary>
    public interface ICategoriesVM : IViewModelBase
    {
        ICommand ShowTagSelectionCommand { get; }

        ICommand ShowShowSelectionCommand { get; }

        ICommand ShowSeriesSelectionCommand { get; }

        ICommand SaveSelectionCommand { get; }

        IViewModelBase CurrentContent { get; }
    }
}