using System.Collections.ObjectModel;

namespace Channel9Downloader.ViewModels.Ribbon
{
    public interface IRibbonComboBoxVM : IRibbonItemVM
    {
        bool IsEditable { get; set; }
        string Text { get; set; }
        ObservableCollection<object> ItemsSource { get; set; }
    }
}