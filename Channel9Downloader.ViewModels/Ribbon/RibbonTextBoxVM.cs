using System.ComponentModel.Composition;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class represents a ribbon textbox viewmodel.
    /// </summary>
    [Export(typeof(IRibbonTextBoxVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonTextBoxVM : RibbonItemVM, IRibbonTextBoxVM
    {
        #region Fields

        /// <summary>
        /// Name of the <see cref="Text"/> property.
        /// </summary>
        public const string PROP_TEXT = "Text";

        /// <summary>
        /// Backing field for <see cref="Text"/> property.
        /// </summary>
        private string _text;

        /// <summary>
        /// Backing field for <see cref="TextBoxWidth"/> property.
        /// </summary>
        private int _textBoxWidth;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                RaisePropertyChanged(PROP_TEXT);
            }
        }

        /// <summary>
        /// Gets or sets the textbox width.
        /// </summary>
        public int TextBoxWidth
        {
            get
            {
                return _textBoxWidth;
            }

            set
            {
                _textBoxWidth = value;
                RaisePropertyChanged(() => TextBoxWidth);
            }
        }

        #endregion Public Properties
    }
}