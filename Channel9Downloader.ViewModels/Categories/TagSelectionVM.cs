using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.Converters;
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
        private CollectionView _tagsCollectionView;

        private ObservableCollection<Tag> _tags;

        #region Public Properties

        /// <summary>
        /// Gets a list of tags.
        /// </summary>
        public CollectionView TagsCollectionView
        {
            get
            {
                return _tagsCollectionView;
            }
            
            set
            {
                _tagsCollectionView = value;
                RaisePropertyChanged(() => TagsCollectionView);
            }
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
            if (_tags != null)
            {
                foreach (var tag in _tags)
                {
                    tag.PropertyChanged -= TagPropertyChanged;
                }
            }

            foreach (var tag in tags)
            {
                tag.PropertyChanged += TagPropertyChanged;
            }

            _tags = new ObservableCollection<Tag>(tags);
            TagsCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(_tags);
            TagsCollectionView.GroupDescriptions.Add(
                new PropertyGroupDescription("IsEnabled", new IsEnabledToDisplayTextConverter()));
            TagsCollectionView.GroupDescriptions.Add(
                new PropertyGroupDescription("Title", new TagTitleToCharacterConverter()));
            TagsCollectionView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void TagPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var category = sender as Tag;

            if (e.PropertyName == Category.PROP_IS_ENABLED)
            {
                _tags.Remove(category);
                _tags.Add(category);
            }
        }

        #endregion Public Methods
    }
}