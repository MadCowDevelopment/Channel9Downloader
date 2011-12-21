using System.ComponentModel.Composition;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class represents a ribbon checkbox viewmodel.
    /// </summary>
    [Export(typeof(IRibbonCheckBoxVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonCheckBoxVM : RibbonItemVM, IRibbonCheckBoxVM
    {
        /// <summary>
        /// Backing field for <see cref="IsChecked"/> property.
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// Backing field for <see cref="Label"/> property.
        /// </summary>
        private string _label;

        /// <summary>
        /// The name of the <see cref="IsChecked"/> property.
        /// </summary>
        public const string PROP_IS_CHECKED = "IsChecked";

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the checkbox is checked.
        /// </summary>
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
