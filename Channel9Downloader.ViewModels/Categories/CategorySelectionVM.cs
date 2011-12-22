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
    /// <summary>
    /// This class handles the selection views for the different categories.
    /// </summary>
    /// <typeparam name="T">The type of category.</typeparam>
    public abstract class CategorySelectionVM<T> : BaseViewModel, ICategorySelectionVM<T>
        where T : Category
    {
        #region Fields

        /// <summary>
        /// The collection of categories.
        /// </summary>
        private ObservableCollection<T> _categories;

        /// <summary>
        /// Backing field for <see cref="CategoriesCollectionView"/> property.
        /// </summary>
        private CollectionView _categoryCollectionView;

        #endregion Fields

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

            private set
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
        /// <param name="filter">The filter that will be applied.</param>
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

        /// <summary>
        /// Refreshes the collection view.
        /// </summary>
        public void Refresh()
        {
            if (CategoriesCollectionView == null)
            {
                return;
            }

            CategoriesCollectionView.Refresh();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Can be overwritten by subclasses and is called after the collections are initialized.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Event handler for the property changed event of tags.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void TagPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var category = sender as T;

            if (e.PropertyName == Category.PROP_IS_ENABLED)
            {
                _categories.Remove(category);
                _categories.Add(category);
            }
        }

        #endregion Private Methods
    }
}