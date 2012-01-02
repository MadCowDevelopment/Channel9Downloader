using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;

using Channel9Downloader.Composition;
using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;
using Channel9Downloader.ViewModels.Ribbon;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the categories view.
    /// </summary>
    [Export(typeof(ICategoriesVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CategoriesVM : AdornerViewModel, ICategoriesVM
    {
        #region Fields

        /// <summary>
        /// The repository used for retrieving categories.
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// The dependency composer used for retrieving instances.
        /// </summary>
        private readonly IDependencyComposer _dependencyComposer;

        /// <summary>
        /// Backing field for <see cref="CurrentContent"/> property.
        /// </summary>
        private IBaseViewModel _currentContent;

        /// <summary>
        /// Backing field for <see cref="SaveSelectionCommand"/> command.
        /// </summary>
        private ICommand _saveSelectionCommand;

        /// <summary>
        /// The series selection viewmodel.
        /// </summary>
        private ISeriesSelectionVM _seriesSelectionVM;

        /// <summary>
        /// The show selection viewmodel.
        /// </summary>
        private IShowSelectionVM _showSelectionVM;

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

        /// <summary>
        /// The tag selection viewmodel.
        /// </summary>
        private ITagSelectionVM _tagSelectionVM;

        /// <summary>
        /// Backing field for <see cref="UpdateCategoriesCommand"/> command.
        /// </summary>
        private ICommand _updateCategoriesCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesVM"/> class.
        /// </summary>
        /// <param name="dependencyComposer">The dependency composer used for creating instances.</param>
        /// <param name="loadingWaitVM">The loading wait viewmodel.</param>
        /// <param name="categoryRepository">The repository used for retrieving categories.</param>
        /// <param name="filterRibbonTextBoxVM">A ribbon text box used for filtering.</param>
        /// <param name="caseSensitiveRibbonCheckBoxVM">The ribbon check box used for setting case sensitive filter.</param>
        [ImportingConstructor]
        public CategoriesVM(
            IDependencyComposer dependencyComposer,
            ILoadingWaitVM loadingWaitVM,
            ICategoryRepository categoryRepository,
            IRibbonTextBoxVM filterRibbonTextBoxVM,
            IRibbonCheckBoxVM caseSensitiveRibbonCheckBoxVM)
        {
            _dependencyComposer = dependencyComposer;
            _categoryRepository = categoryRepository;
            AdornerContent = loadingWaitVM;

            FilterRibbonTextBox = filterRibbonTextBoxVM;
            FilterRibbonTextBox.TextBoxWidth = 100;
            FilterRibbonTextBox.PropertyChanged += FilterRibbonTextBoxPropertyChanged;

            CaseSensitiveRibbonCheckBox = caseSensitiveRibbonCheckBoxVM;
            CaseSensitiveRibbonCheckBox.Label = "case sensitive";
            CaseSensitiveRibbonCheckBox.PropertyChanged += CaseSensitiveRibbonCheckBoxPropertyChanged;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the case sensitive checkbox.
        /// </summary>
        public IRibbonCheckBoxVM CaseSensitiveRibbonCheckBox
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the current content that should be shown in the view.
        /// </summary>
        public IBaseViewModel CurrentContent
        {
            get
            {
                return _currentContent;
            }

            set
            {
                _currentContent = value;
                RaisePropertyChanged(() => CurrentContent);
            }
        }

        /// <summary>
        /// Gets the filter ribbon text box.
        /// </summary>
        public IRibbonTextBoxVM FilterRibbonTextBox
        {
            get; private set;
        }

        /// <summary>
        /// Gets a command that saves the selection.
        /// </summary>
        public ICommand SaveSelectionCommand
        {
            get
            {
                return _saveSelectionCommand
                       ?? (_saveSelectionCommand = new RelayCommand(p => OnSaveSelection(), p => !IsAdornerVisible));
            }
        }

        /// <summary>
        /// Gets a command that shows the series selection.
        /// </summary>
        public ICommand ShowSeriesSelectionCommand
        {
            get
            {
                return _showSeriesSelectionCommand
                       ??
                       (_showSeriesSelectionCommand =
                        new RelayCommand(p => OnShowSeriesSelection(), p => !IsAdornerVisible));
            }
        }

        /// <summary>
        /// Gets a command that shows the show selection.
        /// </summary>
        public ICommand ShowShowSelectionCommand
        {
            get
            {
                return _showShowSelectionCommand
                       ??
                       (_showShowSelectionCommand = new RelayCommand(p => OnShowShowSelection(), p => !IsAdornerVisible));
            }
        }

        /// <summary>
        /// Gets a command that shows the tag selection.
        /// </summary>
        public ICommand ShowTagSelectionCommand
        {
            get
            {
                return _showTagSelectionCommand
                       ??
                       (_showTagSelectionCommand = new RelayCommand(p => OnShowTagSelection(), p => !IsAdornerVisible));
            }
        }

        /// <summary>
        /// Gets a command that updates the categories.
        /// </summary>
        public ICommand UpdateCategoriesCommand
        {
            get
            {
                return _updateCategoriesCommand
                       ??
                       (_updateCategoriesCommand = new RelayCommand(p => OnUpdateCategories(), p => !IsAdornerVisible));
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this class.
        /// </summary>
        public void Initialize()
        {
            InitializeCategoriesAsync(TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Filters the categories using the entered text in the filter text box.
        /// </summary>
        /// <param name="obj">The object to filter.</param>
        /// <returns>Returns true if the object matches the filter criteria, false otherwise.</returns>
        public bool TextFilter(object obj)
        {
            if (string.IsNullOrEmpty(FilterRibbonTextBox.Text))
            {
                return true;
            }

            var category = obj as Category;

            if (category == null)
            {
                return true;
            }

            var filterText = ToLowerIfCaseSensitiveIsActive(FilterRibbonTextBox.Text);

            if (category.Title != null)
            {
                var title = ToLowerIfCaseSensitiveIsActive(category.Title);
                if (title.Contains(filterText))
                {
                    return true;
                }
            }

            if (category.Description != null)
            {
                var title = ToLowerIfCaseSensitiveIsActive(category.Title);
                if (title.Contains(filterText))
                {
                    return true;
                }
            }

            if (category.RelativePath != null)
            {
                var title = ToLowerIfCaseSensitiveIsActive(category.Title);
                if (title.Contains(filterText))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the property changed event of the case sensitive check box.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void CaseSensitiveRibbonCheckBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FilterRibbonTextBox.Text) && e.PropertyName == RibbonCheckBoxVM.PROP_IS_CHECKED)
            {
                RefreshAllLists();
            }
        }

        /// <summary>
        /// Handles the property changed event of the filter ribbon text box.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void FilterRibbonTextBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RibbonTextBoxVM.PROP_TEXT)
            {
                RefreshAllLists();
            }
        }

        /// <summary>
        /// Initializes categories in the background.
        /// </summary>
        /// <param name="continuationTaskScheduler">The task scheduler that should be used for the continuation.
        /// This should usually be the scheduler of the UI thread.</param>
        private void InitializeCategoriesAsync(TaskScheduler continuationTaskScheduler)
        {
            IsAdornerVisible = true;
            Entities.Categories categories = null;
            var task = new Task(() =>
                {
                    categories = _categoryRepository.GetCategories();
                });

            task.ContinueWith(
                x =>
                    {
                        _seriesSelectionVM = _dependencyComposer.GetExportedValue<ISeriesSelectionVM>();
                        _seriesSelectionVM.Initialize(categories.Series, TextFilter);

                        _showSelectionVM = _dependencyComposer.GetExportedValue<IShowSelectionVM>();
                        _showSelectionVM.Initialize(categories.Shows, TextFilter);

                        _tagSelectionVM = _dependencyComposer.GetExportedValue<ITagSelectionVM>();
                        _tagSelectionVM.Initialize(categories.Tags, TextFilter);

                        CurrentContent = _tagSelectionVM;

                        IsAdornerVisible = false;
                        CommandManager.InvalidateRequerySuggested();
                    },
                continuationTaskScheduler);

            task.Start();
        }

        /// <summary>
        /// Saves the selection.
        /// </summary>
        private void OnSaveSelection()
        {
            _categoryRepository.SaveCategories();
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

        /// <summary>
        /// Updates the available categories.
        /// </summary>
        private void OnUpdateCategories()
        {
            UpdateCategoriesAsync(TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Refreshes all category viewmodels.
        /// </summary>
        private void RefreshAllLists()
        {
            _seriesSelectionVM.Refresh();
            _showSelectionVM.Refresh();
            _tagSelectionVM.Refresh();
        }

        /// <summary>
        /// Converts a string to lower case if the case sensitive check box is checked.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>Returns a lower case string if case sensitive is active, the original string otherwise.</returns>
        private string ToLowerIfCaseSensitiveIsActive(string text)
        {
            var result = CaseSensitiveRibbonCheckBox.IsChecked
                                 ? text
                                 : text.ToLower();

            return result;
        }

        /// <summary>
        /// Updates the available categories asynchronously.
        /// </summary>
        /// <param name="continuationTaskScheduler">The task scheduler that should be used for the continuation.
        /// This should usually be the scheduler of the UI thread.</param>
        private void UpdateCategoriesAsync(TaskScheduler continuationTaskScheduler)
        {
            IsAdornerVisible = true;
            var task = new Task(() => _categoryRepository.UpdateCategories());
            task.ContinueWith(x => InitializeCategoriesAsync(continuationTaskScheduler));
            task.Start();
        }

        #endregion Private Methods
    }
}