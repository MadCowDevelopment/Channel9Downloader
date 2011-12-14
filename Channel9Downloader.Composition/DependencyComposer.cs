using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Channel9Downloader.Composition
{
    /// <summary>
    /// This class provides access to the MEF container.
    /// </summary>
    public class DependencyComposer : IDependencyComposer
    {
        #region Fields

        /// <summary>
        /// The MEF composition container.
        /// </summary>
        private readonly CompositionContainer _container;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyComposer"/> class.
        /// </summary>
        public DependencyComposer()
        {
            var catalog = new DirectoryCatalog(".");
            _container = new CompositionContainer(catalog);
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Creates a part from the specified value and composes it in the specified composition container.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <param name="exportedValue">The value to export.</param>
        public void ComposeExportedValue<T>(T exportedValue)
        {
            _container.ComposeExportedValue(exportedValue);
        }

        /// <summary>
        /// Creates composable parts from an array of attributed objects and composes them 
        /// in the specified composition container.
        /// </summary>
        /// <param name="attributedParts">An array of attributed objects to compose.</param>
        public void ComposeParts(params object[] attributedParts)
        {
            _container.ComposeParts(attributedParts);
        }

        /// <summary>
        /// Returns the exported object with the contract name derived from the specified type parameter.
        /// If there is not exactly one matching exported object, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type of the exported object.</typeparam>
        /// <returns>Returns the exported object.</returns>
        public T GetExportedValue<T>()
        {
            return _container.GetExportedValue<T>();
        }

        #endregion Public Methods
    }
}