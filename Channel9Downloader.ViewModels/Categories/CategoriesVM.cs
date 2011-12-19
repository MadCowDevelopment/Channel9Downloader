using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;

using Channel9Downloader.Composition;
using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;

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
        [ImportingConstructor]
        public CategoriesVM(
            IDependencyComposer dependencyComposer,
            ILoadingWaitVM loadingWaitVM,
            ICategoryRepository categoryRepository)
        {
            _dependencyComposer = dependencyComposer;
            _categoryRepository = categoryRepository;
            AdornerContent = loadingWaitVM;
        }

        #endregion Constructors

        #region Public Properties

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

        #endregion Public Methods

        #region Private Methods

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

            task.ContinueWith(x =>
                {
                    _seriesSelectionVM = _dependencyComposer.GetExportedValue<ISeriesSelectionVM>();
                    _seriesSelectionVM.Initialize(categories.Series);

                    _showSelectionVM = _dependencyComposer.GetExportedValue<IShowSelectionVM>();
                    _showSelectionVM.Initialize(categories.Shows);

                    _tagSelectionVM = _dependencyComposer.GetExportedValue<ITagSelectionVM>();
                    _tagSelectionVM.Initialize(categories.Tags);

                    CurrentContent = _tagSelectionVM;

                    IsAdornerVisible = false;
                    CommandManager.InvalidateRequerySuggested();
                }, continuationTaskScheduler);

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