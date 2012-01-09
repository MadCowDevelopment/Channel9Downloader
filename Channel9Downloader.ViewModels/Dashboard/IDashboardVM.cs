using System.Collections.ObjectModel;
using System.Windows.Input;

using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;
using Channel9Downloader.ViewModels.Ribbon;

namespace Channel9Downloader.ViewModels.Dashboard
{
    /// <summary>
    /// This interface is for the dashboard viewmodel.
    /// </summary>
    public interface IDashboardVM : IAdornerViewModel
    {
        #region Properties

        /// <summary>
        /// Gets the command to add a download.
        /// </summary>
        ICommand AddDownloadCommand
        {
            get;
        }

        /// <summary>
        /// Gets a collection of the latest videos.
        /// </summary>
        ObservableCollection<RssItem> LatestVideos
        {
            get;
        }

        /// <summary>
        /// Gets or sets the selected video.
        /// </summary>
        RssItem SelectedVideo
        {
            get; set;
        }

        /// <summary>
        /// Gets the show summary toggle button.
        /// </summary>
        IRibbonToggleButtonVM ShowSummaryRibbonToggleButton
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes this view model.
        /// </summary>
        void Initialize();

        #endregion Methods
    }
}