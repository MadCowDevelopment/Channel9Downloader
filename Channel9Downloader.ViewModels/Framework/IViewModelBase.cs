using System;

namespace MvvmFramework
{
    /// <summary>
    /// This interface is for viewmodel base classes.
    /// </summary>
    public interface IViewModelBase : IValidatingObject, IDisposable
    {
        /// <summary>
        /// Gets the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        string DisplayName { get; }
    }
}