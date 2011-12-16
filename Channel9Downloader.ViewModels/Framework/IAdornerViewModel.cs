namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface is used for a viewmodel that supports an adorner.
    /// </summary>
    public interface IAdornerViewModel : IBaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the adorner content.
        /// </summary>
        IBaseViewModel AdornerContent
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the adorner is visible.
        /// </summary>
        bool IsAdornerVisible
        {
            get; set;
        }

        #endregion Properties
    }
}