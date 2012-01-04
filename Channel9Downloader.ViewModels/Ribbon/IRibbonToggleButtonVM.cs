namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used for the ribbon toggle button.
    /// </summary>
    public interface IRibbonToggleButtonVM : IRibbonButtonVM
    {
        /// <summary>
        /// Gets or sets a value indicating whether the button is checked.
        /// </summary>
        bool IsChecked { get; set; }
    }
}