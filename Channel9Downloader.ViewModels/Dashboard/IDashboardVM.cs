using System.Collections.ObjectModel;
using System.Windows.Input;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Dashboard
{
    using Channel9Downloader.ViewModels.Ribbon;

    /// <summary>
    /// This interface is for the dashboard viewmodel.
    /// </summary>
    public interface IDashboardVM : IAdornerViewModel
    {
        /// <summary>
        /// Gets a collection of the latest videos.
        /// </summary>
        ObservableCollection<RssItem> LatestVideos { get; }

        /// <summary>
        /// Gets or sets the selected video.
        /// </summary>
        RssItem SelectedVideo { get; set; }

        /// <summary>
        /// Gets the show summary toggle button.
        /// </summary>
        IRibbonToggleButtonVM ShowSummaryRibbonToggleButton { get; }

        /// <summary>
        /// Gets the command to add a download.
        /// </summary>
        ICommand AddDownloadCommand { get; }

        /// <summary>
        /// Initializes this view model.
        /// </summary>
        void Initialize();
    }
}