using System;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.Converters;
using Channel9Downloader.Entities;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the tag selection view.
    /// </summary>
    [Export(typeof(ITagSelectionVM))]
    public class TagSelectionVM : CategorySelectionVM<Tag>, ITagSelectionVM
    {
        protected override void OnInitialized()
        {
            CategoriesCollectionView.GroupDescriptions.Add(
                new PropertyGroupDescription("Title", new TagTitleToCharacterConverter()));
        }
    }
}