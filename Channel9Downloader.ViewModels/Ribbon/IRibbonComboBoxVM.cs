using System.Collections.ObjectModel;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used by the ribbon combo box.
    /// </summary>
    public interface IRibbonComboBoxVM : IRibbonItemVM
    {
        /// <summary>
        /// Gets or sets a value indicating whether the combo box is editable.
        /// </summary>
        bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        ObservableCollection<object> ItemsSource { get; set; }
    }
}