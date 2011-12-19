using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Threading;
using Channel9Downloader.DataAccess;
using Channel9Downloader.DataAccess.Events;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class manages the downloads view.
    /// </summary>
    [Export(typeof(IDownloadsVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DownloadsVM : AdornerViewModel, IDownloadsVM
    {
        #region Fields

        /// <summary>
        /// The download manager that loads new videos in the background.
        /// </summary>
        private readonly IDownloadManager _downloadManager;

        /// <summary>
        /// The application settings.
        /// </summary>
        private Settings _settings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadsVM"/> class.
        /// </summary>
        /// <param name="downloadManager">The download manager that loads new videos in the background.</param>
        [ImportingConstructor]
        public DownloadsVM(IDownloadManager downloadManager)
        {
            _downloadManager = downloadManager;
            _downloadManager.DownloadAdded += DownloadManagerDownloadAdded;

            _uiDispatcher = Dispatcher.CurrentDispatcher;

            AdornerContent = new LoadingWaitVM();
            Downloads = new ObservableCollection<DownloadItem>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a list of all downloads.
        /// </summary>
        public ObservableCollection<DownloadItem> Downloads { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        public void Initialize(Settings settings)
        {
            _settings = settings;
            InitializeDownloadManagerAsync();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Initializes categories in the background.
        /// </summary>
        private void InitializeDownloadManagerAsync()
        {
            IsAdornerVisible = true;
            var task = new Task(() => _downloadManager.Initialize(_settings));
            task.ContinueWith(p => IsAdornerVisible = false);
            task.Start();
        }

        /// <summary>
        /// The main thread dispatcher.
        /// </summary>
        private readonly Dispatcher _uiDispatcher;

        /// <summary>
        /// Handles the DownloadAdded event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadManagerDownloadAdded(object sender, DownloadAddedEventArgs e)
        {
            _uiDispatcher.Invoke(new CollectionInitializerDelegate(p => Downloads.Add(p)), e.DownloadItem);
        }

        /// <summary>
        /// This delegate is used for adding items to the download collection.
        /// </summary>
        /// <param name="downloadItem">The download to be added.</param>
        private delegate void CollectionInitializerDelegate(DownloadItem downloadItem);

        #endregion Private Methods
    }
}