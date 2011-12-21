namespace Channel9Downloader.ViewModels.Ribbon
{
    public interface IRibbonTextBoxVM : IRibbonItemVM
    {
        int TextBoxWidth { get; set; }
        string Text { get; set; }
    }
}