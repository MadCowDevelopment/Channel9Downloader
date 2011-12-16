using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;

using Channel9Downloader.Composition;
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
        /// The dependency composer used for retrieving instances.
        /// </summary>
        private readonly IDependencyComposer _dependencyComposer;

        /// <summary>
        /// Backing field for <see cref="CurrentContent"/> property.
        /// </summary>
        private IBaseViewModel _currentContent;

        /// <summary>
        /// Backing field for <see cref="IsBusy"/> property.
        /// </summary>
        private bool _isBusy;

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

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesVM"/> class.
        /// </summary>
        /// <param name="dependencyComposer">The dependency composer used for creating instances.</param>
        /// <param name="loadingWaitVM">The loading wait viewmodel.</param>
        [ImportingConstructor]
        public CategoriesVM(
            IDependencyComposer dependencyComposer,
            ILoadingWaitVM loadingWaitVM)
        {
            _dependencyComposer = dependencyComposer;
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
        /// Gets or sets a value indicating whether the viewmodel is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                _isBusy = value;
                RaisePropertyChanged();
                IsAdornerVisible = value;
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
                       ?? (_saveSelectionCommand = new RelayCommand(p => OnSaveSelection(), p => !IsBusy));
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
                       ?? (_showSeriesSelectionCommand = new RelayCommand(p => OnShowSeriesSelection(), p => !IsBusy));
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
                       ?? (_showShowSelectionCommand = new RelayCommand(p => OnShowShowSelection(), p => !IsBusy));
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
                       ?? (_showTagSelectionCommand = new RelayCommand(p => OnShowTagSelection(), p => !IsBusy));
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
            if (_seriesSelectionVM == null)
            {
                IsBusy = true;
                _seriesSelectionVM = _dependencyComposer.GetExportedValue<ISeriesSelectionVM>();

                var task = new Task(() => _seriesSelectionVM.Initialize());
                task.ContinueWith(x =>
                    {
                        IsBusy = false;
                        CurrentContent = _seriesSelectionVM;
                    });
                task.Start();
            }
            else
            {
                CurrentContent = _seriesSelectionVM;
            }
        }

        /// <summary>
        /// Shows the show selection view.
        /// </summary>
        private void OnShowShowSelection()
        {
            if (_showSelectionVM == null)
            {
                IsBusy = true;
                _showSelectionVM = _dependencyComposer.GetExportedValue<IShowSelectionVM>();

                var task = new Task(() => _showSelectionVM.Initialize());
                task.ContinueWith(x =>
                {
                    IsBusy = false;
                    CurrentContent = _showSelectionVM;
                });
                task.Start();
            }
            else
            {
                CurrentContent = _showSelectionVM;
            }
        }

        /// <summary>
        /// Shows the tag selection view.
        /// </summary>
        private void OnShowTagSelection()
        {
            if (_tagSelectionVM == null)
            {
                IsBusy = true;
                _tagSelectionVM = _dependencyComposer.GetExportedValue<ITagSelectionVM>();

                var task = new Task(() => _tagSelectionVM.Initialize());
                task.ContinueWith(x =>
                {
                    IsBusy = false;
                    CurrentContent = _tagSelectionVM;
                });
                task.Start();
            }
            else
            {
                CurrentContent = _tagSelectionVM;
            }
        }

        #endregion Private Methods
    }
}