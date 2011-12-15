using System;
using System.ComponentModel.Composition;
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
    public class CategoriesVM : ViewModelBase, ICategoriesVM
    {
        private ICommand _showTagSelectionCommand;

        private ICommand _showShowSelectionCommand;

        private ICommand _showSeriesSelectionCommand;

        private ICommand _saveSelectionCommand;

        private IViewModelBase _currentSelectionView;

        private IShowSelectionVM _showSelectionVM;

        private ITagSelectionVM _tagSelectionVM;

        private ISeriesSelectionVM _seriesSelectionVM;

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

        public ICommand ShowTagSelectionCommand
        {
            get
            {
                return _showTagSelectionCommand ?? (_showTagSelectionCommand = new RelayCommand(p => OnShowTagSelection()));
            }
        }

        public ICommand ShowShowSelectionCommand
        {
            get
            {
                return _showShowSelectionCommand ?? (_showShowSelectionCommand = new RelayCommand(p => OnShowShowSelection()));
            }
        }

        public ICommand ShowSeriesSelectionCommand
        {
            get
            {
                return _showSeriesSelectionCommand ?? (_showSeriesSelectionCommand = new RelayCommand(p => OnShowSeriesSelection()));
            }
        }

        public ICommand SaveSelectionCommand
        {
            get
            {
                return _saveSelectionCommand ?? (_saveSelectionCommand = new RelayCommand(p => OnSaveSelection()));
            }
        }

        public IViewModelBase CurrentContent
        {
            get
            {
                return _currentSelectionView;
            }

            set
            {
                _currentSelectionView = value;
                RaisePropertyChanged();
            }
        }

        private void OnShowShowSelection()
        {
            CurrentContent = _showSelectionVM;
        }

        private void OnShowTagSelection()
        {
            CurrentContent = _tagSelectionVM;
        }

        private void OnShowSeriesSelection()
        {
            CurrentContent = _seriesSelectionVM;
        }

        private void OnSaveSelection()
        {
            // TODO: Save selection!!
        }
    }
}