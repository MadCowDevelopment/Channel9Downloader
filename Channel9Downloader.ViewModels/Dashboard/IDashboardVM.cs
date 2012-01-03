using System.Collections.ObjectModel;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Dashboard
{
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
        /// Initializes this view model.
        /// </summary>
        void Initialize();
    }
}