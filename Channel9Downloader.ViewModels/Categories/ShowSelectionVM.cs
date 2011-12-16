using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the show selection view.
    /// </summary>
    [Export(typeof(IShowSelectionVM))]
    public class ShowSelectionVM : BaseViewModel, IShowSelectionVM
    {
        #region Public Properties

        /// <summary>
        /// Gets the collection of shows.
        /// </summary>
        public CollectionView ShowCollectionView
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="shows">The shows which are used to initialize this viewmodel.
        /// </param>
        public void Initialize(List<Show> shows)
        {
            ShowCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(shows);
        }

        #endregion Public Methods
    }
}