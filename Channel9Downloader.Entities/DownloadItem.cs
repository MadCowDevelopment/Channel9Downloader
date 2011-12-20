using System;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about a download item.
    /// </summary>
    public class DownloadItem : ObservableModel
    {
        #region Fields

        /// <summary>
        /// Property name of <see cref="DownloadState"/> property.
        /// </summary>
        public const string PROP_DOWNLOAD_STATE = "DownloadState";

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

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadItem"/> class.
        /// </summary>
        public DownloadItem()
        {
            DownloadState = DownloadState.Queued;
        }

        #endregion Constructors

        #region Public Properties

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

                if (DateTime.Now - _lastUpdate > TimeSpan.FromSeconds(1))
                {
                    CalculateBytesPerSecond();
                    CalculateEta();
                    _lastUpdate = DateTime.Now;
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
                    _downloadStartTime = DateTime.Now;
                    _lastUpdate = DateTime.Now;
                }

                CalculateBytesPerSecond();
                CalculateEta();

                RaisePropertyChanged(PROP_DOWNLOAD_STATE);
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

        #endregion Public Properties

        #region Private Methods

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
                var duration = DateTime.Now - _downloadStartTime;

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