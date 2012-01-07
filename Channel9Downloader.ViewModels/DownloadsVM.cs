using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
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
        /// The list of all downloads.
        /// </summary>
        private readonly ObservableCollection<IDownloadItem> _downloads;

        /// <summary>
        /// The task scheduler of the main thread.
        /// </summary>
        private readonly TaskScheduler _mainThreadTaskScheduler;

        /// <summary>
        /// The main thread dispatcher.
        /// </summary>
        private readonly Dispatcher _mainThreadDispatcher;

        /// <summary>
        /// The timer used for updating the downloads.
        /// </summary>
        private DispatcherTimer _updateTimer;

        /// <summary>
        /// Backing field for <see cref="CleanDownloadsCommand"/> property.
        /// </summary>
        private ICommand _cleanDownloadsCommand;

        /// <summary>
        /// Backing field for <see cref="IsDownloading"/> class.
        /// </summary>
        private bool _isDownloading;

        /// <summary>
        /// The application settings.
        /// </summary>
        private Settings _settings;

        /// <summary>
        /// Backing field for <see cref="StartDownloadsCommand"/> property.
        /// </summary>
        private ICommand _startDownloadsCommand;

        /// <summary>
        /// Backing field for <see cref="StopDownloadsCommand"/> property.
        /// </summary>
        private ICommand _stopDownloadsCommand;

        /// <summary>
        /// Backing field for <see cref="UpdateDownloadsCommand"/> property.
        /// </summary>
        private ICommand _updateDownloadsCommand;

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
            _downloadManager.DownloadRemoved += DownloadManagerDownloadRemoved;
            _downloadManager.DownloadingStarted += DownloadManagerDownloadingStarted;
            _downloadManager.DownloadingStopped += DownloadManagerDownloadingStopped;

            _mainThreadDispatcher = Dispatcher.CurrentDispatcher;
            _mainThreadTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            AdornerContent = new LoadingWaitVM();
            _downloads = new ObservableCollection<IDownloadItem>();
            Downloads = (ListCollectionView)CollectionViewSource.GetDefaultView(_downloads);
            Downloads.SortDescriptions.Add(
                new SortDescription(DownloadItem.PROP_DOWNLOAD_STATE, ListSortDirection.Ascending));
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// This delegate is used for adding items to the download collection.
        /// </summary>
        /// <param name="downloadItem">The download to be added.</param>
        private delegate void CollectionInitializerDelegate(IDownloadItem downloadItem);

        #endregion Delegates

        #region Public Properties

        /// <summary>
        /// Gets a command to clean downloads.
        /// </summary>
        public ICommand CleanDownloadsCommand
        {
            get
            {
                return _cleanDownloadsCommand
                       ?? (_cleanDownloadsCommand = new RelayCommand(p => OnCleanDownloads(), p => !IsAdornerVisible));
            }
        }

        /// <summary>
        /// Gets a list of all downloads.
        /// </summary>
        public ListCollectionView Downloads
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is downloading.
        /// </summary>
        public bool IsDownloading
        {
            get
            {
                return _isDownloading;
            }

            set
            {
                _isDownloading = value;
                RaisePropertyChanged(() => IsDownloading);
            }
        }

        /// <summary>
        /// Gets a command to start downloads.
        /// </summary>
        public ICommand StartDownloadsCommand
        {
            get
            {
                return _startDownloadsCommand
                       ??
                       (_startDownloadsCommand =
                        new RelayCommand(p => OnStartDownloads(), p => !IsAdornerVisible && !IsDownloading));
            }
        }

        /// <summary>
        /// Gets a command to stop downloads.
        /// </summary>
        public ICommand StopDownloadsCommand
        {
            get
            {
                return _stopDownloadsCommand
                       ??
                       (_stopDownloadsCommand =
                        new RelayCommand(p => OnStopDownloads(), p => !IsAdornerVisible && IsDownloading));
            }
        }

        /// <summary>
        /// Gets a command that updates the available downloads.
        /// </summary>
        public ICommand UpdateDownloadsCommand
        {
            get
            {
                return _updateDownloadsCommand
                       ?? (_updateDownloadsCommand = new RelayCommand(p => OnUpdateDownloads(), p => !IsAdornerVisible));
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
            _settings.PropertyChanged += SettingsPropertyChanged;
            InitializeDownloadManagerAsync();

            if (_settings.UpdateVideosPeriodically)
            {
                StartUpdateTimer();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the property changed event of settings.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Settings.PROP_UPDATE_VIDEOS_PERIODICALLY)
            {
                if (_settings.UpdateVideosPeriodically)
                {
                    StartUpdateTimer();
                }
                else
                {
                    StopUpdateTimer();
                }
            }
            else if (e.PropertyName == Settings.PROP_CHECK_FOR_NEW_VIDEOS_INTERVAL)
            {
                if (_updateTimer != null)
                {
                    _updateTimer.Interval = _settings.CheckForNewVideosInterval;
                }
            }
        }

        /// <summary>
        /// Starts the update timer.
        /// </summary>
        private void StartUpdateTimer()
        {
            if (_updateTimer != null)
            {
                return;
            }

            _updateTimer = new DispatcherTimer(
                _settings.CheckForNewVideosInterval,
                DispatcherPriority.Background,
                (sender, args) => UpdateDownloadsAsync(),
                _mainThreadDispatcher);
            _updateTimer.Start();
        }

        /// <summary>
        /// Stops the update timer.
        /// </summary>
        private void StopUpdateTimer()
        {
            if (_updateTimer == null)
            {
                return;
            }

            _updateTimer.Stop();
            _updateTimer = null;
        }

        /// <summary>
        /// Adds a <see cref="DownloadItem"/> on the main thread.
        /// </summary>
        /// <param name="downloadItem">The download to add.</param>
        private void AddDownloadItemOnMainThread(IDownloadItem downloadItem)
        {
            _mainThreadDispatcher.Invoke(new CollectionInitializerDelegate(p => _downloads.Add(p)), downloadItem);
        }

        /// <summary>
        /// Removes and adds an item if the download state has changed.
        /// This is so that the sorting will be applied.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var downloadItem = sender as IDownloadItem;
            if (downloadItem == null)
            {
                return;
            }

            if (e.PropertyName == DownloadItem.PROP_DOWNLOAD_STATE)
            {
                RemoveDownloadItemOnMainThread(downloadItem);
                AddDownloadItemOnMainThread(downloadItem);
            }
        }

        /// <summary>
        /// Adds a download to the list of queued downloads.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadManagerDownloadAdded(object sender, DownloadAddedEventArgs e)
        {
            e.DownloadItem.PropertyChanged += DownloadItemPropertyChanged;
            AddDownloadItemOnMainThread(e.DownloadItem);
        }

        /// <summary>
        /// Sets the download status to true when downloads have started.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadManagerDownloadingStarted(object sender, System.EventArgs e)
        {
            IsDownloading = true;
        }

        /// <summary>
        /// Sets the download status to false when downloads have stopped.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadManagerDownloadingStopped(object sender, System.EventArgs e)
        {
            IsDownloading = false;
        }

        /// <summary>
        /// Removes a download from the list of queued downloads.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void DownloadManagerDownloadRemoved(object sender, DownloadRemovedEventArgs e)
        {
            e.DownloadItem.PropertyChanged -= DownloadItemPropertyChanged;
            RemoveDownloadItemOnMainThread(e.DownloadItem);
        }

        /// <summary>
        /// Initializes categories in the background.
        /// </summary>
        private void InitializeDownloadManagerAsync()
        {
            var continuationTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            IsAdornerVisible = true;
            var task = new Task(() => _downloadManager.Initialize(_settings));
            task.ContinueWith(
                p =>
                    {
                        IsAdornerVisible = false;
                        CommandManager.InvalidateRequerySuggested();
                    },
                continuationTaskScheduler);
            task.Start();
        }

        /// <summary>
        /// Removes downloads that are finished or skipped.
        /// </summary>
        private void OnCleanDownloads()
        {
            if (_downloads == null)
            {
                return;
            }

            for (int i = _downloads.Count - 1; i >= 0; i--)
            {
                var downloadItem = _downloads[i];
                if (downloadItem.DownloadState == DownloadState.Finished ||
                    downloadItem.DownloadState == DownloadState.Skipped)
                {
                    _downloads.Remove(downloadItem);
                    downloadItem.PropertyChanged -= DownloadItemPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Starts the downloads.
        /// </summary>
        private void OnStartDownloads()
        {
            _downloadManager.StartDownloads();
        }

        /// <summary>
        /// Stops the downloads.
        /// </summary>
        private void OnStopDownloads()
        {
            _downloadManager.StopDownloads();
        }

        /// <summary>
        /// Updates the downloads in the background.
        /// </summary>
        private void OnUpdateDownloads()
        {
            UpdateDownloadsAsync();
        }

        /// <summary>
        /// Updates the downloads.
        /// </summary>
        private void UpdateDownloadsAsync()
        {
            if (_downloadManager.IsUpdating)
            {
                return;
            }

            IsAdornerVisible = true;
            var task = new Task(() => _downloadManager.UpdateAvailableDownloads());
            task.ContinueWith(
                p =>
                    {
                        IsAdornerVisible = false;
                        CommandManager.InvalidateRequerySuggested();
                    },
                _mainThreadTaskScheduler);
            task.Start();
        }

        /// <summary>
        /// Removes a <see cref="DownloadItem"/> on the main thread.
        /// </summary>
        /// <param name="downloadItem">The download to remove.</param>
        private void RemoveDownloadItemOnMainThread(IDownloadItem downloadItem)
        {
            _mainThreadDispatcher.Invoke(new CollectionInitializerDelegate(p => _downloads.Remove(p)), downloadItem);
        }

        #endregion Private Methods
    }
}