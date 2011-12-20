using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;

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
        /// The repository used for retrieving categories.
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Gets the download queue (all downloads that have not started yet).
        /// </summary>
        private readonly LinkedList<DownloadItem> _downloadQueue;

        /// <summary>
        /// The repository used for retrieving finished downloads.
        /// </summary>
        private readonly IFinishedDownloadsRepository _finishedDownloadsRepository;

        /// <summary>
        /// The repository used for retrieving RSS items.
        /// </summary>
        private readonly IRssRepository _rssRepository;

        /// <summary>
        /// The downloader used to download data from the web.
        /// </summary>
        private readonly IWebDownloader _webDownloader;

        /// <summary>
        /// The cancellation token for stopping downloads.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

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
        /// <param name="webDownloader">The downloader used to download data from the web.</param>
        /// <param name="finishedDownloadsRepository">The repository used for retrieving finished RSS items.</param>
        [ImportingConstructor]
        public DownloadManager(
            ICategoryRepository categoryRepository,
            IRssRepository rssRepository,
            IWebDownloader webDownloader,
            IFinishedDownloadsRepository finishedDownloadsRepository)
        {
            _categoryRepository = categoryRepository;
            _rssRepository = rssRepository;
            _webDownloader = webDownloader;
            _finishedDownloadsRepository = finishedDownloadsRepository;

            _downloadQueue = new LinkedList<DownloadItem>();
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

        #endregion Events

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
        /// Raises the <see cref="DownloadAdded"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        public void RaiseDownloadAdded(DownloadAddedEventArgs e)
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
        public void RaiseDownloadingStarted(EventArgs e)
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
        public void RaiseDownloadingStopped(EventArgs e)
        {
            var handler = DownloadingStopped;
            if (handler != null)
            {
                handler(this, e);
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

                var downloadItem = _downloadQueue.First.Value;
                _downloadQueue.RemoveFirst();
                var address = GetDownloadAddress(downloadItem);
                var filename = CreateLocalFilename(address);
                var task = _webDownloader.DownloadFileAsync(
                    address, filename, downloadItem, _cancellationTokenSource.Token);
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
                        });
            }
        }

        /// <summary>
        /// Stops downloads.
        /// </summary>
        public void StopDownloads()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Updates all available downloads.
        /// </summary>
        public void UpdateAvailableDownloads()
        {
            var enabledCategories = GetEnabledCategories();
            var availableItems = GetAvailableItems(enabledCategories);
            RemoveAlreadyFinishedDownloads(availableItems);
            EnqueueDownloads(availableItems);
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
        private void EnqueueDownloads(IEnumerable<DownloadItem> availableItems)
        {
            foreach (var availableItem in
                availableItems.Where(
                    availableItem => !_downloadQueue.Any(p => p.RssItem.Guid == availableItem.RssItem.Guid)))
            {
                _downloadQueue.AddLast(availableItem);
                RaiseDownloadAdded(new DownloadAddedEventArgs(availableItem));
            }
        }

        /// <summary>
        /// Gets all items that are available in the RSS repository.
        /// </summary>
        /// <param name="enabledCategories">List of all categories that are enabled.</param>
        /// <returns>Returns a list of all items that are available.</returns>
        private List<DownloadItem> GetAvailableItems(IEnumerable<Category> enabledCategories)
        {
            var availableItems = new List<DownloadItem>();
            foreach (var enabledCategory in enabledCategories)
            {
                var items = _rssRepository.GetRssItems(enabledCategory);
                foreach (var rssItem in items)
                {
                    availableItems.Add(new DownloadItem { Category = enabledCategory, RssItem = rssItem });
                }
            }

            return availableItems;
        }

        /// <summary>
        /// Gets the download address.
        /// </summary>
        /// <param name="downloadItem">The download item.</param>
        /// <returns>Returns the URI of the download item.</returns>
        private string GetDownloadAddress(DownloadItem downloadItem)
        {
            // TODO: chose the correct file depending on user preference
            // TODO: at the moment the smalles file is chosen (this obviously sucks)
            var media = new MediaContent { FileSize = int.MaxValue };
            foreach (var mediaContent in downloadItem.RssItem.MediaGroup.Where(p => p.Medium == "video"))
            {
                if (mediaContent.FileSize < media.FileSize)
                {
                    media = mediaContent;
                }
            }

            return media.Url;
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
        /// Removes already finished downloads from the list of available items.
        /// </summary>
        /// <param name="availableItems">List of all available items.</param>
        private void RemoveAlreadyFinishedDownloads(IList<DownloadItem> availableItems)
        {
            var finishedDownloads = _finishedDownloadsRepository.GetAllFinishedDownloads();
            for (int i = availableItems.Count - 1; i >= 0; i--)
            {
                if (finishedDownloads.Any(p => p.Guid == availableItems[i].RssItem.Guid))
                {
                    availableItems.Remove(availableItems[i]);
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
                // TODO: Start / stop downloads according to max number.
            }
        }

        #endregion Private Methods
    }
}