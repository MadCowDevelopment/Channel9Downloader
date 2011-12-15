using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    [Export(typeof(ITagSelectionVM))]
    public class TagSelectionVM : ViewModelBase, ITagSelectionVM
    {
        [ImportingConstructor]
        public TagSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            var tags = categoryBrowser.GetAllTags();
            TagsCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(tags);
            TagsCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Title", new TitleToCharacterConverter()));
        }

        public CollectionView TagsCollectionView { get; private set; }
    }

    public class TitleToCharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var title = value as string;
            var character = title.Substring(0, 1).ToUpper();
            return character;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
