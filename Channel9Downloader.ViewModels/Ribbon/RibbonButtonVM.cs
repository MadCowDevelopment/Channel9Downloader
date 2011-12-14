using System.Windows.Input;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class manages a ribbon button.
    /// </summary>
    public class RibbonButtonVM : RibbonItemVM, IRibbonButtonVM
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the large image source.
        /// </summary>
        public string LargeImageSource { get; set; }

        /// <summary>
        /// Gets or sets the tool tip description.
        /// </summary>
        public string ToolTipDescription { get; set; }

        /// <summary>
        /// Gets or sets the tool tip title.
        /// </summary>
        public string ToolTipTitle { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command { get; set; }
    }
}
