using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class serves as base class for objects that support validation.
    /// </summary>
    public abstract class ValidatingObject : ObservableObject, IValidatingObject
    {
        #region Fields

        /// <summary>
        /// Property name of <see cref="HasErrors"/> property.
        /// </summary>
        public const string PROP_HAS_ERRORS = "HasErrors";

        /// <summary>
        /// Dictionary containing error messages.
        /// </summary>
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        /// An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public virtual string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object is valid.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return _errors.Count != 0;
            }
        }

        #endregion Public Properties

        #region Indexers

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>
        /// The error message for the property. The default is an empty string ("").
        /// </returns>
        /// <param name="propertyName">The name of the property whose error message to get. </param>
        public string this[string propertyName]
        {
            get
            {
                RemoveError(propertyName);
                ValidateProperty(propertyName);

                RaisePropertyChanged(() => HasErrors);

                return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
            }
        }

        #endregion Indexers

        #region Protected Methods

        /// <summary>
        /// Adds an error to the errors dictionary.
        /// </summary>
        /// <param name="propertyName">Name of the property for which an error should be added.</param>
        /// <param name="message">Error message that will be shown to the user.</param>
        protected void AddError(string propertyName, string message)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = message;
            }
        }

        /// <summary>
        /// Validates a property.
        /// </summary>
        /// <param name="propertyName">Name of the property to validate.</param>
        protected virtual void ValidateProperty(string propertyName)
        {
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Creates the errors dictionary when this class is deserialized.
        /// </summary>
        /// <param name="context">Context of the serialization.</param>
        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            _errors = new Dictionary<string, string>();
        }

        /// <summary>
        /// Removes an error from the errors dictionary.
        /// </summary>
        /// <param name="propertyName">Name of the property to remove.</param>
        private void RemoveError(string propertyName)
        {
            _errors.Remove(propertyName);
        }

        #endregion Private Methods
    }
}