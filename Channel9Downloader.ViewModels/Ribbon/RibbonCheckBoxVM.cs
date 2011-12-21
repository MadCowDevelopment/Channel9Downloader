using System.ComponentModel.Composition;

namespace Channel9Downloader.ViewModels.Ribbon
{
    [Export(typeof(IRibbonCheckBoxVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonCheckBoxVM : RibbonItemVM, IRibbonCheckBoxVM
    {
        private bool _isChecked;

        private string _label;

        public const string PROP_IS_CHECKED = "IsChecked";

        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value;
                RaisePropertyChanged(() => Label);
            }
        }

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                _isChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }
    }
}
