using System;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface is for viewmodel base classes.
    /// </summary>
    public interface IViewModelBase : IValidatingObject, IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        string DisplayName
        {
            get;
        }

        #endregion Properties
    }
}