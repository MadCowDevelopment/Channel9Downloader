using System.ComponentModel.Composition;

namespace Channel9Downloader.ViewModels.Ribbon
{
    [Export(typeof(IRibbonTextBoxVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonTextBoxVM : RibbonItemVM, IRibbonTextBoxVM
    {
        private string _text;

        public const string PROP_TEXT = "Text";

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

        private int _textBoxWidth;

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
    }
}
