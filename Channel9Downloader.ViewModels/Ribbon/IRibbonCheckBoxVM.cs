namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used for the ribbon check box viewmodel.
    /// </summary>
    public interface IRibbonCheckBoxVM : IRibbonItemVM
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the check box is checked.
        /// </summary>
        bool IsChecked { get; set; }
    }
}