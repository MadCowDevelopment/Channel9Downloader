using System.ComponentModel;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This is the interface for observable objects.
    /// </summary>
    public interface IObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Verifies that a property name exists in this ViewModel. This method
        /// can be called before the property is used, for instance before
        /// calling the property changed event invocator. It avoids errors when a
        /// property name is changed but some places are missed.
        /// <para>This method is only active in DEBUG mode.</para>
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        void VerifyPropertyName(string propertyName);
    }
}