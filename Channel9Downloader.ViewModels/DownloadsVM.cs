using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Channel9Downloader.DataAccess;
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
        /// Backing field for <see cref="Downloads"/> property.
        /// </summary>
        private ObservableCollection<DownloadItem> _downloads;

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
            AdornerContent = new LoadingWaitVM();
            Downloads = new ObservableCollection<DownloadItem>();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a list of all downloads.
        /// </summary>
        public ObservableCollection<DownloadItem> Downloads
        {
            get
            {
                return _downloads;
            }

            private set
            {
                _downloads = value;
                RaisePropertyChanged();
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        public void Initialize(Settings settings)
        {
            _settings = settings;
            InitializeDownloadManagerAsync(TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Initializes categories in the background.
        /// </summary>
        /// <param name="continuationTaskScheduler">The task scheduler that should be used for the continuation.
        /// This should usually be the scheduler of the UI thread.</param>
        private void InitializeDownloadManagerAsync(TaskScheduler continuationTaskScheduler)
        {
            IsAdornerVisible = true;
            var task = new Task(() => _downloadManager.Initialize(_settings));

            task.ContinueWith(
                x =>
                    {
                        Downloads = new ObservableCollection<DownloadItem>(_downloadManager.Downloads);
                        IsAdornerVisible = false;
                    },
                continuationTaskScheduler);

            task.Start();
        }

        #endregion Private Methods
    }
}