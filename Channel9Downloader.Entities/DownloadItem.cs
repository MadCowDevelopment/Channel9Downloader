using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

using Channel9Downloader.Common;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a download item.
    /// </summary>
    [Export(typeof(IDownloadItem))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DownloadItem : ObservableModel, IDownloadItem
    {
        #region Fields

        /// <summary>
        /// Property name of <see cref="DownloadState"/> property.
        /// </summary>
        public const string PROP_DOWNLOAD_STATE = "DownloadState";

        /// <summary>
        /// Property name of <see cref="Priority"/> property.
        /// </summary>
        public const string PROP_PRIORITY = "Priority";

        /// <summary>
        /// The DateTime accessor.
        /// </summary>
        private readonly IDate _date;

        /// <summary>
        /// Backing field for <see cref="BytesPerSecond"/> property.
        /// </summary>
        private double _bytesPerSecond;

        /// <summary>
        /// Backing field for <see cref="BytesReceived"/> property.
        /// </summary>
        private long _bytesReceived;

        /// <summary>
        /// The time when the download has started.
        /// </summary>
        private DateTime _downloadStartTime;

        /// <summary>
        /// Backing field for <see cref="DownloadState"/> property.
        /// </summary>
        private DownloadState _downloadState;

        /// <summary>
        /// Saves the last update of download speed and ETA.
        /// </summary>
        private DateTime _lastUpdate;

        /// <summary>
        /// Backing field for <see cref="Priority"/> property.
        /// </summary>
        private DownloadPriority _priority;

        /// <summary>
        /// Backing field for <see cref="ProgressPercentage"/> property.
        /// </summary>
        private int _progressPercentage;

        /// <summary>
        /// Backing field for <see cref="RemainingTime"/> property.
        /// </summary>
        private TimeSpan _remainingTime;

        /// <summary>
        /// Backing field for <see cref="TotalBytesToReceive"/> property.
        /// </summary>
        private long _totalBytesToReceive;

        /// <summary>
        /// Backing field for <see cref="SkipCommand"/> property.
        /// </summary>
        private ICommand _skipCommand;

        /// <summary>
        /// Backing field for <see cref="QueueCommand"/> property.
        /// </summary>
        private ICommand _queueCommand;

        /// <summary>
        /// Backing field for <see cref="PlayMovieCommand"/> property.
        /// </summary>
        private ICommand _playMovieCommand;

        /// <summary>
        /// Backing field for <see cref="OpenInExplorerCommand"/> property.
        /// </summary>
        private ICommand _openInExplorerCommand;

        /// <summary>
        /// Backing field for <see cref="LocalFilename"/> property.
        /// </summary>
        private string _localFilename;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadItem"/> class.
        /// </summary>
        /// <param name="date">The date and time accessor.</param>
        [ImportingConstructor]
        public DownloadItem(IDate date)
        {
            _date = date;

            DownloadState = DownloadState.Queued;
            Priority = DownloadPriority.Normal;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets a command to play the movie in the default player.
        /// </summary>
        public ICommand PlayMovieCommand
        {
            get
            {
                return _playMovieCommand
                       ??
                       (_playMovieCommand =
                        new RelayCommand(p => OnPlayMovie(), p => DownloadState == DownloadState.Finished));
            }
        }

        /// <summary>
        /// Gets a command to open the item in the windows explorer.
        /// </summary>
        public ICommand OpenInExplorerCommand
        {
            get
            {
                return _openInExplorerCommand
                       ??
                       (_openInExplorerCommand =
                        new RelayCommand(p => OnOpenInExplorer(), p => DownloadState == DownloadState.Finished));
            }
        }

        /// <summary>
        /// Gets a command to skip the download.
        /// </summary>
        public ICommand SkipCommand
        {
            get
            {
                return _skipCommand
                       ??
                       (_skipCommand =
                        new RelayCommand(
                            p => DownloadState = DownloadState.Skipped,
                            p => DownloadState == DownloadState.Queued || DownloadState == DownloadState.Stopped));
            }
        }

        /// <summary>
        /// Gets a command to queue a skipped download.
        /// </summary>
        public ICommand QueueCommand
        {
            get
            {
                return _queueCommand
                       ??
                       (_queueCommand =
                        new RelayCommand(
                            p => DownloadState = DownloadState.Queued, p => DownloadState == DownloadState.Skipped));
            }
        }

        /// <summary>
        /// Gets or sets the average bytes per second download speed.
        /// </summary>
        public double BytesPerSecond
        {
            get
            {
                return _bytesPerSecond;
            }

            set
            {
                _bytesPerSecond = value;
                RaisePropertyChanged(() => BytesPerSecond);
            }
        }

        /// <summary>
        /// Gets or sets the number of bytes received.
        /// </summary>
        public long BytesReceived
        {
            get
            {
                return _bytesReceived;
            }

            set
            {
                _bytesReceived = value;
                RaisePropertyChanged(() => BytesReceived);

                if (_date.Now - _lastUpdate > TimeSpan.FromSeconds(1))
                {
                    CalculateBytesPerSecond();
                    CalculateEta();
                    _lastUpdate = _date.Now;
                }
            }
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current download state.
        /// </summary>
        public DownloadState DownloadState
        {
            get
            {
                return _downloadState;
            }

            set
            {
                _downloadState = value;

                if (_downloadState == DownloadState.Downloading)
                {
                    _downloadStartTime = _date.Now;
                    _lastUpdate = _date.Now;
                }

                CalculateBytesPerSecond();
                CalculateEta();

                RaisePropertyChanged(PROP_DOWNLOAD_STATE);
            }
        }

        /// <summary>
        /// Gets or sets the download priority.
        /// </summary>
        public DownloadPriority Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                if (value == _priority)
                {
                    return;
                }

                _priority = value;
                RaisePropertyChanged(() => Priority);
            }
        }

        /// <summary>
        /// Gets or sets the download progress in percent.
        /// </summary>
        public int ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }

            set
            {
                _progressPercentage = value;
                RaisePropertyChanged(() => ProgressPercentage);
            }
        }

        /// <summary>
        /// Gets or sets the remaining time in seconds.
        /// </summary>
        public TimeSpan RemainingTime
        {
            get
            {
                return _remainingTime;
            }

            set
            {
                _remainingTime = value;
                RaisePropertyChanged(() => RemainingTime);
            }
        }

        /// <summary>
        /// Gets or sets the RSS item.
        /// </summary>
        public RssItem RssItem
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total size of the download.
        /// </summary>
        public long TotalBytesToReceive
        {
            get
            {
                return _totalBytesToReceive;
            }

            set
            {
                if (_totalBytesToReceive == value)
                {
                    return;
                }

                _totalBytesToReceive = value;
                RaisePropertyChanged(() => TotalBytesToReceive);
            }
        }

        /// <summary>
        /// Gets or sets the filename of the item on the local harddisc.
        /// </summary>
        public string LocalFilename
        {
            get
            {
                return _localFilename;
            }

            set
            {
                if (_localFilename != value)
                {
                    _localFilename = value;
                    RaisePropertyChanged(() => LocalFilename);
                }
            }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Plays the movie in the default player.
        /// </summary>
        private void OnPlayMovie()
        {
            if (!File.Exists(LocalFilename))
            {
                return;
            }

            Process.Start(LocalFilename);
        }

        /// <summary>
        /// Opens the item in the windows explorer.
        /// </summary>
        private void OnOpenInExplorer()
        {
            if (!File.Exists(LocalFilename))
            {
                return;
            }

            var arguments = string.Format("/select,\"{0}\"", LocalFilename);
            Process.Start("explorer.exe", arguments);
        }

        /// <summary>
        /// Calculates the bytes per second.
        /// </summary>
        private void CalculateBytesPerSecond()
        {
            if (DownloadState != DownloadState.Downloading ||
                _downloadStartTime == default(DateTime) ||
                TotalBytesToReceive == 0)
            {
                BytesPerSecond = 0;
            }
            else
            {
                var duration = _date.Now - _downloadStartTime;

                var bytesPerSecond = BytesReceived / duration.TotalSeconds;

                BytesPerSecond = double.IsInfinity(bytesPerSecond) ? 0 : bytesPerSecond;
            }
        }

        /// <summary>
        /// Calculates the remaining time until the download is finished.
        /// </summary>
        private void CalculateEta()
        {
            if (DownloadState != DownloadState.Downloading ||
                ProgressPercentage >= 100 ||
                BytesPerSecond == 0.0)
            {
                RemainingTime = TimeSpan.FromSeconds(0);
            }
            else
            {
                var remainingBytes = TotalBytesToReceive - BytesReceived;
                var eta = remainingBytes / BytesPerSecond;
                RemainingTime = TimeSpan.FromSeconds(eta);
            }
        }

        #endregion Private Methods
    }
}