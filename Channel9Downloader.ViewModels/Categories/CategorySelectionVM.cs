using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Channel9Downloader.Converters;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    public class CategorySelectionVM<T> : BaseViewModel, ICategorySelectionVM<T> where T : Category
    {
        private CollectionView _categoryCollectionView;

        private ObservableCollection<T> _categories;

        #region Public Properties

        /// <summary>
        /// Gets a list of tags.
        /// </summary>
        public CollectionView CategoriesCollectionView
        {
            get
            {
                return _categoryCollectionView;
            }

            set
            {
                _categoryCollectionView = value;
                RaisePropertyChanged(() => CategoriesCollectionView);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="categories">The categories which are used to initialize this viewmodel.
        /// </param>
        public void Initialize(List<T> categories, Predicate<object> filter)
        {
            if (_categories != null)
            {
                foreach (var tag in _categories)
                {
                    tag.PropertyChanged -= TagPropertyChanged;
                }
            }

            foreach (var tag in categories)
            {
                tag.PropertyChanged += TagPropertyChanged;
            }

            _categories = new ObservableCollection<T>(categories);
            CategoriesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(_categories);
            CategoriesCollectionView.GroupDescriptions.Add(
                new PropertyGroupDescription("IsEnabled", new IsEnabledToDisplayTextConverter()));
            CategoriesCollectionView.SortDescriptions.Add(new SortDescription("IsEnabled", ListSortDirection.Descending));
            CategoriesCollectionView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            CategoriesCollectionView.Filter = filter;
            OnInitialized();
        }

        public void Refresh()
        {
            if (CategoriesCollectionView == null)
            {
                return;
            }

            CategoriesCollectionView.Refresh();
        }

        protected virtual void OnInitialized()
        {
        }

        private void TagPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var category = sender as T;

            if (e.PropertyName == Category.PROP_IS_ENABLED)
            {
                _categories.Remove(category);
                _categories.Add(category);
            }
        }

        #endregion Public Methods
    }
}
