using System.Collections.ObjectModel;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class represents a ribbon combobox.
    /// </summary>
    public class RibbonComboBoxVM : RibbonItemVM, IRibbonComboBoxVM
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the combobox is editable.
        /// </summary>
        public bool IsEditable
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        public ObservableCollection<object> ItemsSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get; set;
        }

        #endregion Public Properties
    }
}