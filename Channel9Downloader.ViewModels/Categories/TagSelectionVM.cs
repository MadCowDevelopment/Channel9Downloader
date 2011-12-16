using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.Converters;
using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the tag selection view.
    /// </summary>
    [Export(typeof(ITagSelectionVM))]
    public class TagSelectionVM : BaseViewModel, ITagSelectionVM
    {
        #region Public Properties

        /// <summary>
        /// Gets a list of tags.
        /// </summary>
        public CollectionView TagsCollectionView
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this view.
        /// </summary>
        /// <param name="tags">The tags which are used to initialize this viewmodel.
        /// </param>
        public void Initialize(List<Tag> tags)
        {
            TagsCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(tags);
            TagsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Title", new TagTitleToCharacterConverter()));
        }

        #endregion Public Methods
    }
}