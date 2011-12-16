using System.Windows.Input;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This interface is for the categories viewmodel.
    /// </summary>
    public interface ICategoriesVM : IAdornerViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current content that should be shown in the view.
        /// </summary>
        IBaseViewModel CurrentContent
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the viewmodel is busy.
        /// </summary>
        bool IsBusy
        {
            get; set;
        }

        /// <summary>
        /// Gets a command that saves the selection.
        /// </summary>
        ICommand SaveSelectionCommand
        {
            get;
        }

        /// <summary>
        /// Gets a command that shows the series selection.
        /// </summary>
        ICommand ShowSeriesSelectionCommand
        {
            get;
        }

        /// <summary>
        /// Gets a command that shows the show selection.
        /// </summary>
        ICommand ShowShowSelectionCommand
        {
            get;
        }

        /// <summary>
        /// Gets a command that shows the tag selection.
        /// </summary>
        ICommand ShowTagSelectionCommand
        {
            get;
        }

        #endregion Properties
    }
}