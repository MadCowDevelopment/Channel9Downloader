namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This interface is used by ribbon text box.
    /// </summary>
    public interface IRibbonTextBoxVM : IRibbonItemVM
    {
        #region Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        string Text
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the textbox width.
        /// </summary>
        int TextBoxWidth
        {
            get; set;
        }

        #endregion Properties
    }
}