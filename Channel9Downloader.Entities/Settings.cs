﻿using System;
using System.Runtime.Serialization;

namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class holds information about the application settings.
    /// </summary>
    [DataContract]
    public class Settings : ObservableModel
    {
        #region Fields

        /// <summary>
        /// Property name for <see cref="CheckForNewVideosInterval"/> property.
        /// </summary>
        public const string PROP_CHECK_FOR_NEW_VIDEOS_INTERVAL = "CheckForNewVideosInterval";

        /// <summary>
        /// Property name for <see cref="MaximumParallelDownloads"/> property.
        /// </summary>
        public const string PROP_MAXIMUM_PARALLEL_DOWNLOADS = "MaximumParallelDownloads";

        /// <summary>
        /// Property name for <see cref="UpdateVideosPeriodically"/> property.
        /// </summary>
        public const string PROP_UPDATE_VIDEOS_PERIODICALLY = "UpdateVideosPeriodically";

        /// <summary>
        /// Backing field for <see cref="CheckForNewVideosInterval"/> property.
        /// </summary>
        private TimeSpan _checkForNewVideosInterval;

        /// <summary>
        /// Backing field for <see cref="DownloadFolder"/> property.
        /// </summary>
        private string _downloadFolder;

        /// <summary>
        /// Backing field for <see cref="MaximumParallelDownloads"/> property.
        /// </summary>
        private int _maximumParallelDownloads;

        /// <summary>
        /// Backing field for <see cref="StartDownloadingWhenApplicationStarts"/> property.
        /// </summary>
        private bool _startDownloadingWhenApplicationStarts;

        /// <summary>
        /// Backing field for <see cref="UpdateVideosPeriodically"/> property.
        /// </summary>
        private bool _updateVideosPeriodically;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            DownloadFolder = string.Empty;
            MaximumParallelDownloads = 2;
            CheckForNewVideosInterval = TimeSpan.FromHours(1);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the interval for how often new videos are loaded.
        /// </summary>
        [DataMember]
        public TimeSpan CheckForNewVideosInterval
        {
            get
            {
                return _checkForNewVideosInterval;
            }

            set
            {
                _checkForNewVideosInterval = value;
                RaisePropertyChanged(() => CheckForNewVideosInterval);
            }
        }

        /// <summary>
        /// Gets or sets the folder where movies will be downloaded to.
        /// </summary>
        [DataMember]
        public string DownloadFolder
        {
            get
            {
                return _downloadFolder;
            }

            set
            {
                _downloadFolder = value;
                RaisePropertyChanged(() => DownloadFolder);
            }
        }

        /// <summary>
        /// Gets or sets the number of parallel downloads.
        /// </summary>
        [DataMember]
        public int MaximumParallelDownloads
        {
            get
            {
                return _maximumParallelDownloads;
            }

            set
            {
                _maximumParallelDownloads = value;
                RaisePropertyChanged(PROP_MAXIMUM_PARALLEL_DOWNLOADS);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to start downloading when the application starts.
        /// </summary>
        [DataMember]
        public bool StartDownloadingWhenApplicationStarts
        {
            get
            {
                return _startDownloadingWhenApplicationStarts;
            }

            set
            {
                _startDownloadingWhenApplicationStarts = value;
                RaisePropertyChanged(() => StartDownloadingWhenApplicationStarts);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether videos will be updated periodically.
        /// </summary>
        [DataMember]
        public bool UpdateVideosPeriodically
        {
            get
            {
                return _updateVideosPeriodically;
            }

            set
            {
                _updateVideosPeriodically = value;
                RaisePropertyChanged(() => UpdateVideosPeriodically);
            }
        }

        #endregion Public Properties
    }
}