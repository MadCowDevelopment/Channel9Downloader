namespace Channel9Downloader.Composition
{
    /// <summary>
    /// Interface for the MEF dependency composer.
    /// </summary>
    public interface IDependencyComposer
    {
        #region Methods

        /// <summary>
        /// Creates a part from the specified value and composes it in the specified composition container.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <param name="exportedValue">The value to export.</param>
        void ComposeExportedValue<T>(T exportedValue);

        /// <summary>
        /// Creates composable parts from an array of attributed objects and composes them 
        /// in the specified composition container.
        /// </summary>
        /// <param name="attributedParts">An array of attributed objects to compose.</param>
        void ComposeParts(params object[] attributedParts);

        /// <summary>
        /// Returns the exported object with the contract name derived from the specified type parameter.
        /// If there is not exactly one matching exported object, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type of the exported object.</typeparam>
        /// <returns>Returns the exported object.</returns>
        T GetExportedValue<T>();

        #endregion Methods
    }
}