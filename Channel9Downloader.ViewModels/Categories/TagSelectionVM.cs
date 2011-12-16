using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.Converters;
using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the tag selection view.
    /// </summary>
    [Export(typeof(ITagSelectionVM))]
    public class TagSelectionVM : ViewModelBase, ITagSelectionVM
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagSelectionVM"/> class.
        /// </summary>
        /// <param name="categoryBrowser">The category browser.</param>
        [ImportingConstructor]
        public TagSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            var tags = categoryBrowser.GetAllTags();
            TagsCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(tags);
            TagsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Title", new TagTitleToCharacterConverter()));
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a list of tags.
        /// </summary>
        public CollectionView TagsCollectionView
        {
            get; private set;
        }

        #endregion Public Properties
    }
}