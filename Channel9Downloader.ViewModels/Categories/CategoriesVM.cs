using System.ComponentModel.Composition;
using System.Windows.Input;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the categories view.
    /// </summary>
    [Export(typeof(ICategoriesVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CategoriesVM : ViewModelBase, ICategoriesVM
    {
        #region Fields

        /// <summary>
        /// The series selection viewmodel.
        /// </summary>
        private readonly ISeriesSelectionVM _seriesSelectionVM;

        /// <summary>
        /// The show selection viewmodel.
        /// </summary>
        private readonly IShowSelectionVM _showSelectionVM;

        /// <summary>
        /// The tag selection viewmodel.
        /// </summary>
        private readonly ITagSelectionVM _tagSelectionVM;

        /// <summary>
        /// Backing field for <see cref="CurrentContent"/> property.
        /// </summary>
        private IViewModelBase _currentContent;

        /// <summary>
        /// Backing field for <see cref="SaveSelectionCommand"/> command.
        /// </summary>
        private ICommand _saveSelectionCommand;

        /// <summary>
        /// Backing field for <see cref="ShowSeriesSelectionCommand"/> command.
        /// </summary>
        private ICommand _showSeriesSelectionCommand;

        /// <summary>
        /// Backing field for <see cref="ShowShowSelectionCommand"/> command.
        /// </summary>
        private ICommand _showShowSelectionCommand;

        /// <summary>
        /// Backing field for <see cref="ShowTagSelectionCommand"/> command.
        /// </summary>
        private ICommand _showTagSelectionCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesVM"/> class.
        /// </summary>
        /// <param name="tagSelectionVM">The tag selection viewmodel.</param>
        /// <param name="showSelectionVM">The show selection viewmodel.</param>
        /// <param name="seriesSelectionVM">The series selection viewmodel.</param>
        [ImportingConstructor]
        public CategoriesVM(
            ITagSelectionVM tagSelectionVM,
            IShowSelectionVM showSelectionVM,
            ISeriesSelectionVM seriesSelectionVM)
        {
            _tagSelectionVM = tagSelectionVM;
            _showSelectionVM = showSelectionVM;
            _seriesSelectionVM = seriesSelectionVM;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the current content that should be shown in the view.
        /// </summary>
        public IViewModelBase CurrentContent
        {
            get
            {
                return _currentContent;
            }

            set
            {
                _currentContent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a command that saves the selection.
        /// </summary>
        public ICommand SaveSelectionCommand
        {
            get
            {
                return _saveSelectionCommand ?? (_saveSelectionCommand = new RelayCommand(p => OnSaveSelection()));
            }
        }

        /// <summary>
        /// Gets a command that shows the series selection.
        /// </summary>
        public ICommand ShowSeriesSelectionCommand
        {
            get
            {
                return _showSeriesSelectionCommand ?? (_showSeriesSelectionCommand = new RelayCommand(p => OnShowSeriesSelection()));
            }
        }

        /// <summary>
        /// Gets a command that shows the show selection.
        /// </summary>
        public ICommand ShowShowSelectionCommand
        {
            get
            {
                return _showShowSelectionCommand ?? (_showShowSelectionCommand = new RelayCommand(p => OnShowShowSelection()));
            }
        }

        /// <summary>
        /// Gets a command that shows the tag selection.
        /// </summary>
        public ICommand ShowTagSelectionCommand
        {
            get
            {
                return _showTagSelectionCommand ?? (_showTagSelectionCommand = new RelayCommand(p => OnShowTagSelection()));
            }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Saves the selection.
        /// </summary>
        private void OnSaveSelection()
        {
            // TODO: Save selection!!
        }

        /// <summary>
        /// Shows the series selection view.
        /// </summary>
        private void OnShowSeriesSelection()
        {
            CurrentContent = _seriesSelectionVM;
        }

        /// <summary>
        /// Shows the show selection view.
        /// </summary>
        private void OnShowShowSelection()
        {
            CurrentContent = _showSelectionVM;
        }

        /// <summary>
        /// Shows the tag selection view.
        /// </summary>
        private void OnShowTagSelection()
        {
            CurrentContent = _tagSelectionVM;
        }

        #endregion Private Methods
    }
}