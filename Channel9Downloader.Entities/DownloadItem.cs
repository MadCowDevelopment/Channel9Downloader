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
        /// Backing field for <see cref="ProgressPercentage"/> property.
        /// </summary>
        private int _progressPercentage;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category
        {
            get;
            set;
        }

        private DownloadState _downloadState;

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
                else if (_downloadState == DownloadState.Finished)
                {
                    CalculateBytesPerSecond();
                    CalculateEta();
                }

                RaisePropertyChanged();
            }
        }

        private DateTime _downloadStartTime;

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
                RaisePropertyChanged();
            }
        }

        private double _bytesPerSecond;

        public double BytesPerSecond
        {
            get
            {
                return _bytesPerSecond;
            }

            set
            {
                _bytesPerSecond = value;
                RaisePropertyChanged();
            }
        }

        private long _bytesReceived;

        private long _totalBytesToReceive;

        public long BytesReceived
        {
            get
            {
                return _bytesReceived;
            }

            set
            {
                _bytesReceived = value;
                RaisePropertyChanged();

                if (DateTime.Now - _lastUpdate > TimeSpan.FromSeconds(1))
                {
                    CalculateBytesPerSecond();
                    CalculateEta();
                    _lastUpdate = DateTime.Now;
                }
            }
        }

        private DateTime _lastUpdate;

        private void CalculateEta()
        {
            if (DownloadState != DownloadState.Downloading ||
                ProgressPercentage >= 100)
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

        private void CalculateBytesPerSecond()
        {
            if (DownloadState != DownloadState.Downloading  ||
                _downloadStartTime == default(DateTime) ||
                TotalBytesToReceive == 0)
            {
                BytesPerSecond = 0;
            }
            else
            {
                var duration = DateTime.Now - _downloadStartTime;

                var bytesPerSecond = BytesReceived / duration.TotalSeconds;
                BytesPerSecond = bytesPerSecond;
            }
        }

        private TimeSpan _remainingTime;

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
                RaisePropertyChanged();
            }
        }

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
                RaisePropertyChanged();
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

        #endregion Public Properties
    }
}