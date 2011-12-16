namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface describes methods for loading and writing objects to/from files.
    /// </summary>
    /// <typeparam name="T">Type of the object to serialize/deserialize.</typeparam>
    public interface IDataContractDataAccess<T>
    {
        #region Methods

        /// <summary>
        /// Load an object of type T.
        /// </summary>
        /// <param name="filename">File to open.</param>
        /// <returns>Returns the deserialized object of type T.</returns>
        T Load(string filename);

        /// <summary>
        /// Save an object of type T.
        /// </summary>
        /// <param name="data">Object to save.</param>
        /// <param name="filename">Filename of the serialized object.</param>
        void Save(T data, string filename);

        #endregion Methods
    }
}