using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;

using Channel9Downloader.Common;
using Channel9Downloader.Composition;
using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Dashboard
{
    using Channel9Downloader.ViewModels.Ribbon;

    /// <summary>
    /// This class manages the dashboard view.
    /// </summary>
    [Export(typeof(IDashboardVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DashboardVM : AdornerViewModel, IDashboardVM
    {
        /// <summary>
        /// The dependency composer.
        /// </summary>
        private readonly IDependencyComposer _composer;

        /// <summary>
        /// The repository used for accessing RSS data.
        /// </summary>
        private readonly IRssRepository _rssRepository;

        /// <summary>
        /// The download manager.
        /// </summary>
        private readonly IDownloadManager _downloadManager;

        /// <summary>
        /// Backing field for <see cref="LatestVideos"/> property.
        /// </summary>
        private ObservableCollection<RssItem> _latestVideos;

        /// <summary>
        /// Backing field for <see cref="SelectedVideo"/> property.
        /// </summary>
        private RssItem _selectedVideo;

        /// <summary>
        /// Backing field for <see cref="ShowSummaryCommand"/> property.
        /// </summary>
        private ICommand _showSummaryCommand;

        /// <summary>
        /// Backing field for <see cref="AddDownloadCommand"/> property.
        /// </summary>
        private ICommand _addDownloadCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardVM"/> class.
        /// </summary>
        /// <param name="composer">The dependency composer.</param>
        /// <param name="rssRepository">The repository used for accessing RSS data.</param>
        /// <param name="downloadManager">The download manager used for downloading items.</param>
        /// <param name="showSummaryToggleButtonVM">The toggle button for hide/show summary.</param>
        [ImportingConstructor]
        public DashboardVM(
            IDependencyComposer composer,
            IRssRepository rssRepository,
            IDownloadManager downloadManager,
            IRibbonToggleButtonVM showSummaryToggleButtonVM)
        {
            _composer = composer;
            _rssRepository = rssRepository;
            _downloadManager = downloadManager;

            ShowSummaryRibbonToggleButton = showSummaryToggleButtonVM;
            ShowSummaryRibbonToggleButton.Command = ShowSummaryCommand;
            ShowSummaryRibbonToggleButton.IsChecked = true;
            ShowSummaryRibbonToggleButton.Label = "Summary";
            ShowSummaryRibbonToggleButton.LargeImageSource =
                @"..\Images\Dashboard\SpeechBubble.png";
            ShowSummaryRibbonToggleButton.ToolTipDescription = "Shows/hides the summary speech bubble.";
            ShowSummaryRibbonToggleButton.ToolTipTitle = "Show/hide summary";

            AdornerContent = new LoadingWaitVM();
        }

        /// <summary>
        /// Gets a command to show the summary.
        /// </summary>
        public ICommand ShowSummaryCommand
        {
            get
            {
                return _showSummaryCommand ?? (_showSummaryCommand = new RelayCommand(p => { }));
            }
        }

        /// <summary>
        /// Gets a collection of the latest videos.
        /// </summary>
        public ObservableCollection<RssItem> LatestVideos
        {
            get
            {
                return _latestVideos;
            }

            private set
            {
                _latestVideos = value;
                RaisePropertyChanged(() => LatestVideos);
            }
        }

        /// <summary>
        /// Gets or sets the selected video.
        /// </summary>
        public RssItem SelectedVideo
        {
            get
            {
                return _selectedVideo;
            }

            set
            {
                _selectedVideo = value;
                RaisePropertyChanged(() => SelectedVideo);
            }
        }

        /// <summary>
        /// Gets the show summary ribbon toggle button.
        /// </summary>
        public IRibbonToggleButtonVM ShowSummaryRibbonToggleButton { get; private set; }

        /// <summary>
        /// Gets the command to add a download.
        /// </summary>
        public ICommand AddDownloadCommand
        {
            get
            {
                return _addDownloadCommand
                       ?? (_addDownloadCommand = new RelayCommand(p => OnAddDownload(), p => SelectedVideo != null));
            }
        }

        /// <summary>
        /// Initializes this view model.
        /// </summary>
        public void Initialize()
        {
            InitializeLatestVideosAsync(TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Adds the selected video to the downloads.
        /// </summary>
        private void OnAddDownload()
        {
            var downloadItem = _composer.GetExportedValue<IDownloadItem>();
            downloadItem.RssItem = SelectedVideo;
            _downloadManager.AddDownload(downloadItem);
        }

        /// <summary>
        /// Initializes the latest videos in the background.
        /// </summary>
        /// <param name="continuationTaskScheduler">The main thread scheduler.</param>
        private void InitializeLatestVideosAsync(TaskScheduler continuationTaskScheduler)
        {
            IsAdornerVisible = true;
            var latestVideos = new List<RssItem>();
            var task = new Task(() =>
            {
                latestVideos = _rssRepository.GetRssItems();
            });

            task.ContinueWith(
                x =>
                {
                    LatestVideos = new ObservableCollection<RssItem>(latestVideos);
                    IsAdornerVisible = false;
                    CommandManager.InvalidateRequerySuggested();
                },
                continuationTaskScheduler);

            task.Start();
        }
    }
}
