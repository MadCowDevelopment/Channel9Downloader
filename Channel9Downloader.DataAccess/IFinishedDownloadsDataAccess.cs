using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods to load/save <see cref="FinishedDownloads"/>.
    /// </summary>
    public interface IFinishedDownloadsDataAccess
    {
        #region Methods

        /// <summary>
        /// Deserializes <see cref="FinishedDownloads"/> from the specified file.
        /// </summary>
        /// <param name="filename">The file which contains the serialized data.</param>
        /// <returns>Returns a deserialized <see cref="FinishedDownloads"/> instance.</returns>
        FinishedDownloads LoadFinishedDownloads(string filename);

        /// <summary>
        /// Serializes the <see cref="FinishedDownloads"/> to the specified file.
        /// </summary>
        /// <param name="finishedDownloads">The instance to serialize.</param>
        /// <param name="filename">The serialization target file.</param>
        void SaveFinishedDownloads(FinishedDownloads finishedDownloads, string filename);

        #endregion Methods
    }
}