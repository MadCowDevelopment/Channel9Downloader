namespace Channel9Downloader.ViewModels.Ribbon
{
    public interface IRibbonCheckBoxVM : IRibbonItemVM
    {
        string Label { get; set; }

        bool IsChecked { get; set; }
    }
}