using System.ComponentModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel9Downloader.Test.Utils
{
    /// <summary>
    /// This class contains additional asertion methods.
    /// </summary>
    public static class Assertion
    {
        /// <summary>
        /// Asserts that property changed is called for the specified property when the value of the property is set
        /// to the specified value.
        /// </summary>
        /// <param name="observableObject">The object that will be observed.</param>
        /// <param name="propertyName">The name of the property that will be observed.</param>
        /// <param name="value">The value to which the property should be set.</param>
        public static void PropertyChangedIsCalled(
            INotifyPropertyChanged observableObject,
            string propertyName,
            object value)
        {
            PropertyChangedIsCalled(observableObject, propertyName, value, true);
        }

        /// <summary>
        /// Asserts that property changed is not called for the specified property when the value of the property is set
        /// to the specified value.
        /// </summary>
        /// <param name="observableObject">The object that will be observed.</param>
        /// <param name="propertyName">The name of the property that will be observed.</param>
        /// <param name="value">The value to which the property should be set.</param>
        public static void PropertyChangedIsNotCalled(
            INotifyPropertyChanged observableObject,
            string propertyName,
            object value)
        {
            PropertyChangedIsCalled(observableObject, propertyName, value, false);
        }

        /// <summary>
        /// Asserts that property changed is called for the specified property when the value of the property is set
        /// to the specified value.
        /// </summary>
        /// <param name="observableObject">The object that will be observed.</param>
        /// <param name="propertyName">The name of the property that will be observed.</param>
        /// <param name="value">The value to which the property should be set.</param>
        /// <param name="isRaised">Determines wether property changed should be called or not.</param>
        private static void PropertyChangedIsCalled(
            INotifyPropertyChanged observableObject,
            string propertyName,
            object value,
            bool isRaised)
        {
            var propertyFound = false;
            var propertyChangedCalled = false;
            observableObject.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    propertyChangedCalled = true;
                }
            };

            var properties = observableObject.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name == propertyName)
                {
                    propertyInfo.SetValue(observableObject, value, null);
                    propertyFound = true;
                    break;
                }
            }

            Assert.IsTrue(
                propertyFound,
                string.Format("The property {0} could not be found on object {1}.", propertyName, observableObject));

            Assert.AreEqual(isRaised, propertyChangedCalled);
        }
    }
}
