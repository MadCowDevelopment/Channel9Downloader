using System;
using System.Collections.Generic;
using System.Windows.Data;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    public interface ICategorySelectionVM<T> : IBaseViewModel where T : Category
    {
        /// <summary>
        /// Gets a list of tags.
        /// </summary>
        CollectionView CategoriesCollectionView { get; set; }

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="categories">The categories which are used to initialize this viewmodel.
        /// </param>
        /// <param name="filter">The filter that should be applied.</param>
        void Initialize(List<T> categories, Predicate<object> filter);

        void Refresh();
    }
}