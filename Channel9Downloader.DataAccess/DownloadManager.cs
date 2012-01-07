using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Channel9Downloader.Composition;
using Channel9Downloader.DataAccess.Events;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class manages all downloads.
    /// </summary>
    [Export(typeof(IDownloadManager))]
    public class DownloadManager : IDownloadManager
    {
        #region Fields

        /// <summary>
        /// Dictionary containing tokens for stopping downloads.
        /// </summary>
        private readonly Dictionary<IDownloadItem, CancellationTokenSource> _cancellationTokenSources;

        /// <summary>
        /// The repository used for retrieving categories.
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// The composer used for creating instances.
        /// </summary>
        private readonly IDependencyComposer _composer;

        /// <summary>
        /// Gets the download queue (all downloads that have not started yet).
        /// </summary>
        private readonly LinkedList<IDownloadItem> _downloadQueue;

        /// <summary>
        /// The repository used for retrieving finished downloads.
        /// </summary>
        private readonly IFinishedDownloadsRepository _finishedDownloadsRepository;

        /// <summary>
        /// The repository used for retrieving RSS items.
        /// </summary>
        private readonly IRssRepository _rssRepository;

        /// <summary>
        /// The number of running downloads.
        /// </summary>
        private int _numberOfRunningDownloads;

        /// <summary>
        /// The application settings.
        /// </summary>
        private Settings _settings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadManager"/> class.
        /// </summary>
        /// <param name="categoryRepository">The repository used for retrieving categories.</param>
        /// <param name="rssRepository">The repository used for retrieving RSS items.</param>
        /// <param name="finishedDownloadsRepository">The repository used for retrieving finished RSS items.</param>
        /// <param name="composer">The composer used for creating instances.</param>
        [ImportingConstructor]
        public DownloadManager(
            ICategoryRepository categoryRepository,
            IRssRepository rssRepository,
            IFinishedDownloadsRepository finishedDownloadsRepository,
            IDependencyComposer composer)
        {
            _categoryRepository = categoryRepository;
            _rssRepository = rssRepository;
            _finishedDownloadsRepository = finishedDownloadsRepository;
            _composer = composer;

            _downloadQueue = new LinkedList<IDownloadItem>();
            _cancellationTokenSources = new Dictionary<IDownloadItem, CancellationTokenSource>();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// This event is raised when a download has finished.
        /// </summary>
        public event EventHandler<DownloadAddedEventArgs> DownloadAdded;

        /// <summary>
        /// This event is raised when downloading has started.
        /// </summary>
        public event EventHandler<EventArgs> DownloadingStarted;

        /// <summary>
        /// This event is raised when downloading has stopped.
        /// </summary>
        public event EventHandler<EventArgs> DownloadingStopped;

        /// <summary>
        /// This event is raised when a download is removed.
        /// </summary>
        public event EventHandler<DownloadRemovedEventArgs> DownloadRemoved;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the download manager is currently updating;
        /// </summary>
        public bool IsUpdating { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        public void Initialize(Settings settings)
        {
            _settings = settings;
            _settings.PropertyChanged += SettingsPropertyChanged;

            UpdateAvailableDownloads();

            if (_settings.StartDownloadingWhenApplicationStarts)
            {
                StartDownloads();
            }
        }

        /// <summary>
        /// Starts downloads.
        /// </summary>
        public void StartDownloads()
        {
            while (_numberOfRunningDownloads < _settings.MaximumParallelDownloads && _downloadQueue.Count > 0)
            {
                _numberOfRunningDownloads++;

                if (_numberOfRunningDownloads == 1)
                {
                    RaiseDownloadingStarted(new EventArgs());
                }

                var downloadItem = GetNextDownload();
                _downloadQueue.Remove(downloadItem);

                var cancellationTokenSource = new CancellationTokenSource();
                _cancellationTokenSources.Add(downloadItem, cancellationTokenSource);
                var address = GetDownloadAddress(downloadItem);
                var filename = CreateLocalFilename(address);
                var task = DownloadFileAsync(address, filename, downloadItem, cancellationTokenSource.Token);
                task.ContinueWith(
                    x =>
                    {
                        _numberOfRunningDownloads--;

                        if (_numberOfRunningDownloads == 0)
                        {
                            RaiseDownloadingStopped(new EventArgs());
                        }

                        if (x.IsCanceled)
                        {
                            _downloadQueue.AddFirst(downloadItem);
                        }
                        else
                        {
                            _finishedDownloadsRepository.AddFinishedDownload(downloadItem.RssItem);
                            StartDownloads();
                        }

                        _cancellationTokenSources.Remove(downloadItem);
                    });
            }
        }

        /// <summary>
        /// Stops downloads.
        /// </summary>
        public void StopDownloads()
        {
            var tokenSources = _cancellationTokenSources.ToList();

            foreach (var keyValuePair in tokenSources)
            {
                keyValuePair.Value.Cancel();
            }
        }

        /// <summary>
        /// Updates all available downloads.
        /// </summary>
        public void UpdateAvailableDownloads()
        {
            if (IsUpdating)
            {
                return;
            }

            IsUpdating = true;

            var enabledCategories = GetEnabledCategories().ToList();
            var availableItems = GetAvailableItems(enabledCategories);
            RemoveAlreadyFinishedDownloads(availableItems);
            RemoveDownloadsFromQueueThatAreNoLongerEnabled(enabledCategories);
            EnqueueDownloads(availableItems);

            IsUpdating = false;
        }

        /// <summary>
        /// Adds a download at the end of the queue.
        /// </summary>
        /// <param name="downloadItem">The download to add.</param>
        public void AddDownload(IDownloadItem downloadItem)
        {
            if (_downloadQueue.Any(p => p.RssItem.Guid == downloadItem.RssItem.Guid))
            {
                return;
            }

            _downloadQueue.AddLast(downloadItem);
            RaiseDownloadAdded(new DownloadAddedEventArgs(downloadItem));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the filename where the download should be copied to.
        /// </summary>
        /// <param name="address">Address of the download.</param>
        /// <returns>Returns the local filename.</returns>
        private string CreateLocalFilename(string address)
        {
            var uri = new Uri(address);
            var path = Path.Combine(_settings.DownloadFolder, uri.Segments[uri.Segments.Length - 1]);
            return path;
        }

        /// <summary>
        /// Enqueues all downloads that are not already queued.
        /// </summary>
        /// <param name="availableItems">List of available items.</param>
        private void EnqueueDownloads(IEnumerable<IDownloadItem> availableItems)
        {
            foreach (var availableItem in
                availableItems.Where(
                    availableItem =>
                        !_downloadQueue.Any(p => p.RssItem.Guid == availableItem.RssItem.Guid) &&
                        !_cancellationTokenSources.Keys.Any(p => p.RssItem.Guid == availableItem.RssItem.Guid)))
            {
                AddDownload(availableItem);
            }
        }

        /// <summary>
        /// Gets the next download item. First it will look for items with high priority, then normal priority
        /// and at last low priority.
        /// </summary>
        /// <returns>Returns the next download item.</returns>
        private IDownloadItem GetNextDownload()
        {
            foreach (var downloadItem in _downloadQueue)
            {
                if (downloadItem.Priority == DownloadPriority.High)
                {
                    return downloadItem;
                }
            }

            foreach (var downloadItem in _downloadQueue)
            {
                if (downloadItem.Priority == DownloadPriority.Normal)
                {
                    return downloadItem;
                }
            }

            return _downloadQueue.First.Value;
        }

        /// <summary>
        /// Gets all items that are available in the RSS repository.
        /// </summary>
        /// <param name="enabledCategories">List of all categories that are enabled.</param>
        /// <returns>Returns a list of all items that are available.</returns>
        private List<IDownloadItem> GetAvailableItems(IEnumerable<Category> enabledCategories)
        {
            var availableItems = new List<IDownloadItem>();
            foreach (var enabledCategory in enabledCategories)
            {
                var items = _rssRepository.GetRssItems(enabledCategory);
                foreach (var rssItem in items.Where(p => p.MediaGroup.Count > 0))
                {
                    var downloadItem = _composer.GetExportedValue<IDownloadItem>();
                    downloadItem.Category = enabledCategory;
                    downloadItem.RssItem = rssItem;
                    availableItems.Add(downloadItem);
                }
            }

            return availableItems;
        }

        /// <summary>
        /// Gets the download address.
        /// </summary>
        /// <param name="downloadItem">The download item.</param>
        /// <returns>Returns the URI of the download item.</returns>
        private string GetDownloadAddress(IDownloadItem downloadItem)
        {
            var media = new MediaContent { FileSize = 0 };
            foreach (var mediaContent in downloadItem.RssItem.MediaGroup)
            {
                if (mediaContent.FileSize > media.FileSize)
                {
                    media = mediaContent;
                }
            }

            return media.Url;
        }

        /// <summary>
        /// Downloads a file asynchronously.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <param name="filename">The name of the local file that is to receive the data.</param>
        /// <param name="downloadItem">The download item.</param>
        /// <param name="token">The token for cancelling the operation.</param>
        /// <returns>Returns a Task.</returns>
        private Task<object> DownloadFileAsync(
            string address,
            string filename,
            IDownloadItem downloadItem,
            CancellationToken token)
        {
            var tcs = new TaskCompletionSource<object>();
            var webClient = _composer.GetExportedValue<IWebDownloader>();

            token.Register(webClient.CancelAsync);

            webClient.DownloadFileCompleted += (obj, args) =>
            {
                if (args.Cancelled)
                {
                    tcs.TrySetCanceled();
                    downloadItem.DownloadState = DownloadState.Stopped;
                    return;
                }

                if (args.Error != null)
                {
                    tcs.TrySetException(args.Error);
                    downloadItem.DownloadState = DownloadState.Error;
                    return;
                }

                tcs.TrySetResult(null);
                downloadItem.DownloadState = DownloadState.Finished;
            };

            webClient.DownloadProgressChanged += (obj, args) =>
            {
                downloadItem.ProgressPercentage = args.ProgressPercentage;
                downloadItem.BytesReceived = args.BytesReceived;
                downloadItem.TotalBytesToReceive = args.TotalBytesToReceive;
            };

            try
            {
                webClient.DownloadFileAsync(new Uri(address), filename);
                downloadItem.DownloadState = DownloadState.Downloading;
            }
            catch (UriFormatException ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Get all categories that are enabled.
        /// </summary>
        /// <returns>Returns all enabled categories.</returns>
        private IEnumerable<Category> GetEnabledCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var enabledTags = categories.Tags.Where(p => p.IsEnabled);
            var enabledShows = categories.Shows.Where(p => p.IsEnabled);
            var enabledSeries = categories.Series.Where(p => p.IsEnabled);

            var enabledCategories = new List<Category>(enabledTags);
            enabledCategories.AddRange(enabledShows);
            enabledCategories.AddRange(enabledSeries);

            return enabledCategories;
        }

        /// <summary>
        /// Raises the <see cref="DownloadAdded"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadAdded(DownloadAddedEventArgs e)
        {
            var handler = DownloadAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadingStarted"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadingStarted(EventArgs e)
        {
            var handler = DownloadingStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadingStopped"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadingStopped(EventArgs e)
        {
            var handler = DownloadingStopped;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadRemoved"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        private void RaiseDownloadRemoved(DownloadRemovedEventArgs e)
        {
            var handler = DownloadRemoved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Removes already finished downloads from the list of available items.
        /// </summary>
        /// <param name="availableItems">List of all available items.</param>
        private void RemoveAlreadyFinishedDownloads(IList<IDownloadItem> availableItems)
        {
            var finishedDownloads = _finishedDownloadsRepository.GetAllFinishedDownloads();
            for (int i = availableItems.Count - 1; i >= 0; i--)
            {
                int i1 = i;
                if (finishedDownloads.Any(p => p.Guid == availableItems[i1].RssItem.Guid))
                {
                    availableItems.Remove(availableItems[i]);
                }
            }
        }

        /// <summary>
        /// Removes all downloads from the queue whose category is no longer enabled.
        /// </summary>
        /// <param name="enabledCategories">List of enabled categories.</param>
        private void RemoveDownloadsFromQueueThatAreNoLongerEnabled(IEnumerable<Category> enabledCategories)
        {
            for (int i = _downloadQueue.Count - 1; i >= 0; i--)
            {
                var downloadItem = _downloadQueue.ElementAt(i);

                if (downloadItem.Category == null)
                {
                    continue;
                }

                var isInAnyCategory =
                    enabledCategories.Any(p => p.RelativePath == downloadItem.Category.RelativePath);

                if (!isInAnyCategory &&
                    (downloadItem.DownloadState == DownloadState.Queued || downloadItem.DownloadState == DownloadState.Stopped))
                {
                    _downloadQueue.Remove(_downloadQueue.ElementAt(i));
                    RaiseDownloadRemoved(new DownloadRemovedEventArgs(downloadItem));
                }
            }
        }

        /// <summary>
        /// Event handler for property changed of settings.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Settings.PROP_MAXIMUM_PARALLEL_DOWNLOADS)
            {
                StartDownloads();
            }
        }

        #endregion Private Methods
    }
}