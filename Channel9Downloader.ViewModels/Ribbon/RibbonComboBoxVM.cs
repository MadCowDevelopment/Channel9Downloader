using System.Collections.ObjectModel;

namespace Channel9Downloader.ViewModels.Ribbon
{
    public class RibbonComboBoxVM : RibbonItemVM, IRibbonComboBoxVM
    {
        public bool IsEditable { get; set; }

        public string Text { get; set; }

        public ObservableCollection<object> ItemsSource
        {
            get;
            set;
        }
    }
}
