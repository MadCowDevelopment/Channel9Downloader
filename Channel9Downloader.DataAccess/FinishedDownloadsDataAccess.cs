using System.ComponentModel.Composition;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class serializes / deserializes <see cref="FinishedDownloads"/> instances.
    /// </summary>
    [Export(typeof(IFinishedDownloadsDataAccess))]
    public class FinishedDownloadsDataAccess : DataContractDataAccess<FinishedDownloads>, IFinishedDownloadsDataAccess
    {
        #region Public Methods

        /// <summary>
        /// Deserializes <see cref="FinishedDownloads"/> from the specified file.
        /// </summary>
        /// <param name="filename">The file which contains the serialized data.</param>
        /// <returns>Returns a deserialized <see cref="FinishedDownloads"/> instance.</returns>
        public FinishedDownloads LoadFinishedDownloads(string filename)
        {
            return Load(filename);
        }

        /// <summary>
        /// Serializes the <see cref="FinishedDownloads"/> to the specified file.
        /// </summary>
        /// <param name="finishedDownloads">The instance to serialize.</param>
        /// <param name="filename">The serialization target file.</param>
        public void SaveFinishedDownloads(FinishedDownloads finishedDownloads, string filename)
        {
            Save(finishedDownloads, filename);
        }

        #endregion Public Methods
    }
}