using System;
using System.Diagnostics;
using System.Globalization;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>
    public abstract class BaseViewModel : ValidatingObject, IBaseViewModel
    {
        #region Fields

        /// <summary>
        /// Indicates whether this instance has been removed.
        /// </summary>
        private bool _disposed;

        #endregion Fields

        #if DEBUG

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseViewModel"/> class. 
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", 
            Justification = "This finalizer will only be executed in debug mode.")]
        ~BaseViewModel()
        {
            string msg = string.Format(
                CultureInfo.CurrentCulture, "{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
            Debug.WriteLine(msg);
            Dispose(false);
        }

        #endif

        #region Public Properties

        /// <summary>
        /// Gets or sets the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName
        {
            get; protected set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Disposes managed and unmanaged resources. 
        /// </summary>
        /// <param name="disposing">Determines whether dispose has been called from user code (<see langword="true"/>) 
        /// or from a finalizer (<see langword="false"/>).</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            _disposed = true;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseViewModel"/> class.
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        #endregion Protected Methods
    }
}