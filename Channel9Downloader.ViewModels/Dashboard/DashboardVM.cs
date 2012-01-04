using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;

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
        /// The repository used for accessing RSS data.
        /// </summary>
        private readonly IRssRepository _rssRepository;

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
        /// Initializes a new instance of the <see cref="DashboardVM"/> class.
        /// </summary>
        /// <param name="rssRepository">The repository used for accessing RSS data.</param>
        /// <param name="showSummaryToggleButtonVM">The toggle button for hide/show summary.</param>
        [ImportingConstructor]
        public DashboardVM(
            IRssRepository rssRepository,
            IRibbonToggleButtonVM showSummaryToggleButtonVM)
        {
            _rssRepository = rssRepository;

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
        /// Initializes this view model.
        /// </summary>
        public void Initialize()
        {
            InitializeLatestVideosAsync(TaskScheduler.FromCurrentSynchronizationContext());
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
