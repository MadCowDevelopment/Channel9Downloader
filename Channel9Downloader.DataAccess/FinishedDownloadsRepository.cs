using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class allows access to <see cref="FinishedDownloads"/>.
    /// </summary>
    [Export(typeof(IFinishedDownloadsRepository))]
    public class FinishedDownloadsRepository : IFinishedDownloadsRepository
    {
        #region Fields

        /// <summary>
        /// The filename of the serialized repository.
        /// </summary>
        private readonly string _filename;

        /// <summary>
        /// The <see cref="FinishedDownloads"/> in the repository.
        /// </summary>
        private readonly FinishedDownloads _finishedDownloads;

        /// <summary>
        /// The data access to finished downloads.
        /// </summary>
        private readonly IFinishedDownloadsDataAccess _finishedDownloadsDataAccess;

        /// <summary>
        /// The folder utils for retrieving special folders.
        /// </summary>
        private readonly IFolderUtils _folderUtils;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FinishedDownloadsRepository"/> class.
        /// </summary>
        /// <param name="finishedDownloadsDataAccess">The data access to finished downloads.</param>
        /// <param name="folderUtils">The folder utils for retrieving special folders.</param>
        [ImportingConstructor]
        public FinishedDownloadsRepository(
            IFinishedDownloadsDataAccess finishedDownloadsDataAccess,
            IFolderUtils folderUtils)
        {
            _finishedDownloadsDataAccess = finishedDownloadsDataAccess;
            _folderUtils = folderUtils;

            _filename = Path.Combine(_folderUtils.GetUserDataPath(), "FinishedDownloads.data");
            _finishedDownloads = _finishedDownloadsDataAccess.LoadFinishedDownloads(_filename);
            if (_finishedDownloads == null)
            {
                _finishedDownloads = new FinishedDownloads();
                _finishedDownloadsDataAccess.SaveFinishedDownloads(_finishedDownloads, _filename);
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Adds a finished download to the repository.
        /// </summary>
        /// <param name="rssItem">The item to add.</param>
        public void AddFinishedDownload(RssItem rssItem)
        {
            _finishedDownloads.Items.Add(rssItem);
            _finishedDownloadsDataAccess.SaveFinishedDownloads(_finishedDownloads, _filename);
        }

        /// <summary>
        /// Gets a list of all finished downloads.
        /// </summary>
        /// <returns>Returns a list of all finished downloads.</returns>
        public List<RssItem> GetAllFinishedDownloads()
        {
            return _finishedDownloads.Items;
        }

        #endregion Public Methods
    }
}