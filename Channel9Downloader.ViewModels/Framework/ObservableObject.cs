using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// A base class for the ViewModel classes in the MVVM pattern.
    /// </summary>
    public abstract class ObservableObject : IObservableObject
    {
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Protected Properties

        /// <summary>
        /// Gets access to the <see cref="PropertyChanged"/> event handler to derived classes.
        /// </summary>
        protected PropertyChangedEventHandler PropertyChangedHandler
        {
            get
            {
                return PropertyChanged;
            }
        }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Verifies that a property name exists in this ViewModel. This method
        /// can be called before the property is used, for instance before
        /// calling <see cref="RaisePropertyChanged"/>. It avoids errors when a
        /// property name is changed but some places are missed.
        /// <para>This method is only active in DEBUG mode.</para>
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            var type = GetType();
            if (type.GetProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event if needed.
        /// </summary>
        /// <remarks>If the <paramref name="propertyName"/> parameter
        /// does not correspond to an existing property on the current class, an
        /// exception is thrown in DEBUG configuration only.</remarks>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changed.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        [SuppressMessage(
            "Microsoft.Design",
            "CA1006:GenericMethodsShouldProvideTypeParameter",
            Justification = "This syntax is more convenient than other alternatives.")]
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                return;
            }

            var handler = PropertyChanged;

            if (handler != null)
            {
                var body = propertyExpression.Body as MemberExpression;
                if (body != null)
                {
                    var expression = body.Expression as ConstantExpression;
                    if (expression != null)
                    {
                        handler(expression.Value, new PropertyChangedEventArgs(body.Member.Name));
                    }
                }
            }
        }

        /// <summary>
        /// When called in a property setter, raises the <see cref="PropertyChanged"/> event for 
        /// the current property.
        /// </summary>
        /// <exception cref="InvalidOperationException">If this method is called outside
        /// of a property setter.</exception>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        protected virtual void RaisePropertyChanged()
        {
            var frames = new StackTrace();

            for (var i = 0; i < frames.FrameCount; i++)
            {
                var frame = frames.GetFrame(i).GetMethod() as MethodInfo;
                if (frame != null)
                {
                    if (frame.IsSpecialName && frame.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
                    {
                        RaisePropertyChanged(frame.Name.Substring(4));
                        return;
                    }
                }
            }

            throw new InvalidOperationException("This method can only by invoked within a property setter.");
        }

        #endregion Protected Methods
    }
}