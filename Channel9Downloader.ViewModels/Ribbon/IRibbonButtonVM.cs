using System.Windows.Input;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used for ribbon buttons.
    /// </summary>
    public interface IRibbonButtonVM : IRibbonItemVM
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the large image source.
        /// </summary>
        string LargeImageSource { get; set; }

        /// <summary>
        /// Gets or sets the tool tip description.
        /// </summary>
        string ToolTipDescription { get; set; }

        /// <summary>
        /// Gets or sets the tool tip title.
        /// </summary>
        string ToolTipTitle { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        ICommand Command { get; set; }
    }
}
