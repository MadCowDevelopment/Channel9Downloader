using System.ComponentModel.Composition;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class represents a ribbon toggle button.
    /// </summary>
    [Export(typeof(IRibbonToggleButtonVM))]
    public class RibbonToggleButtonVM : RibbonButtonVM, IRibbonToggleButtonVM
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="IsChecked"/> property.
        /// </summary>
        private bool _isChecked;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the button is checked.
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

        #endregion Public Properties
    }
}