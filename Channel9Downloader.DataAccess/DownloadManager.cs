﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

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
        private readonly Queue<DownloadItem> _downloadQueue;

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
        /// The available categories.
        /// </summary>
        private Categories _categories;

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

            _downloadQueue = new Queue<DownloadItem>();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// This event is raised when a download has finished.
        /// </summary>
        public event EventHandler<DownloadAddedEventArgs> DownloadAdded;

        #endregion Events

        #region Public Methods

        /// <summary>
        /// Get all categories that are enabled.
        /// </summary>
        /// <returns>Returns all enabled categories.</returns>
        public List<Category> GetEnabledCategories()
        {
            var enabledTags = _categories.Tags.Where(p => p.IsEnabled);
            var enabledShows = _categories.Shows.Where(p => p.IsEnabled);
            var enabledSeries = _categories.Series.Where(p => p.IsEnabled);

            var enabledCategories = new List<Category>(enabledTags);
            enabledCategories.AddRange(enabledShows);
            enabledCategories.AddRange(enabledSeries);

            return enabledCategories;
        }

        /// <summary>
        /// Initializes this class.
        /// </summary>
        /// <param name="settings">The application settings.</param>
        public void Initialize(Settings settings)
        {
            _settings = settings;
            _settings.PropertyChanged += SettingsPropertyChanged;
            _categories = _categoryRepository.GetCategories();
            var enabledCategories = GetEnabledCategories();

            var availableItems = new List<DownloadItem>();
            foreach (var enabledCategory in enabledCategories)
            {
                var items = _rssRepository.GetRssItems(enabledCategory);
                foreach (var rssItem in items)
                {
                    availableItems.Add(new DownloadItem { Category = enabledCategory, RssItem = rssItem });
                }
            }

            var finishedDownloads = _finishedDownloadsRepository.GetAllFinishedDownloads();

            for (int i = availableItems.Count - 1; i >= 0; i--)
            {
                if (finishedDownloads.Any(p => p.Guid == availableItems[i].RssItem.Guid))
                {
                    availableItems.Remove(availableItems[i]);
                }
            }

            // Add all downloads that are not already on the list.
            foreach (var availableItem in
                availableItems.Where(availableItem => !_downloadQueue.Any(p => p.RssItem.Guid == availableItem.RssItem.Guid)))
            {
                _downloadQueue.Enqueue(availableItem);
                RaiseDownloadAdded(new DownloadAddedEventArgs(availableItem));
            }

            StartDownloads();
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
        /// Gets the download address.
        /// </summary>
        /// <param name="downloadItem">The download item.</param>
        /// <returns>Returns the URI of the download item.</returns>
        private string GetDownloadAddress(DownloadItem downloadItem)
        {
            // TODO: chose the correct file depending on user preference
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

        /// <summary>
        /// Starts downloads.
        /// </summary>
        private void StartDownloads()
        {
            while (_numberOfRunningDownloads < _settings.MaximumParallelDownloads && _downloadQueue.Count > 0)
            {
                _numberOfRunningDownloads++;

                var downloadItem = _downloadQueue.Dequeue();
                var address = GetDownloadAddress(downloadItem);
                var filename = CreateLocalFilename(address);
                var task = _webDownloader.DownloadFileAsync(address, filename, downloadItem);
                task.ContinueWith(
                    x =>
                        {
                            _numberOfRunningDownloads--;
                            _finishedDownloadsRepository.AddFinishedDownload(downloadItem.RssItem);
                            StartDownloads();
                        });
            }
        }

        #endregion Private Methods
    }
}