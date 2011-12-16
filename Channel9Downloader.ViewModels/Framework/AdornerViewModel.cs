namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class provides properties for an adorned viewmodel.
    /// </summary>
    public class AdornerViewModel : BaseViewModel, IAdornerViewModel
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="AdornerContent"/> property.
        /// </summary>
        private IBaseViewModel _adornerContent;

        /// <summary>
        /// Backing field for <see cref="IsAdornerVisible"/> property.
        /// </summary>
        private bool _isAdornerVisible;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the adorner content.
        /// </summary>
        public IBaseViewModel AdornerContent
        {
            get
            {
                return _adornerContent;
            }

            set
            {
                _adornerContent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the adorner is visible.
        /// </summary>
        public bool IsAdornerVisible
        {
            get
            {
                return _isAdornerVisible;
            }

            set
            {
                _isAdornerVisible = value;
                RaisePropertyChanged();
            }
        }

        #endregion Public Properties
    }
}