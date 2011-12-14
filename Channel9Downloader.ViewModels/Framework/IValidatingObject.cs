using System.ComponentModel;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This is the interface for validating objects.
    /// </summary>
    public interface IValidatingObject : IObservableObject, IDataErrorInfo
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the object is valid.
        /// </summary>
        bool HasErrors
        {
            get;
        }

        #endregion Properties
    }
}