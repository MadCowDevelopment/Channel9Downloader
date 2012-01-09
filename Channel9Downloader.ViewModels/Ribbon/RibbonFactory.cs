using System.Collections.Generic;
using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Categories;
using Channel9Downloader.ViewModels.Dashboard;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class creates the ribbon.
    /// </summary>
    [Export(typeof(IRibbonFactory))]
    public class RibbonFactory : IRibbonFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the categories viewmodel.
        /// </summary>
        [Import]
        public ICategoriesVM CategoriesVM
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the dashboard viewmodel.
        /// </summary>
        [Import]
        public IDashboardVM DashboardVM
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the downloads viewmodel.
        /// </summary>
        [Import]
        public IDownloadsVM DownloadsVM
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the main window viewmodel.
        /// </summary>
        [Import]
        public IMainWindowVM MainWindowVM
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates the ribbon tabs.
        /// </summary>
        /// <returns>Returns a list of all ribbon tabs.</returns>
        public List<IRibbonTabVM> CreateRibbonTabs()
        {
            var result = new List<IRibbonTabVM>();
            result.Add(CreateDashboardTab());
            result.Add(CreateCategoryTab());
            result.Add(CreateDownloadTab());
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the category tab.
        /// </summary>
        /// <returns>Returns the category tab.</returns>
        private RibbonTabVM CreateCategoryTab()
        {
            var categories = new RibbonTabVM();
            categories.Header = RibbonTabName.CATEGORIES;

            // Set up group 'Categories'
            var groupCategories = new RibbonGroupVM();
            groupCategories.Header = "Categories";

            var buttonTags = new RibbonButtonVM();
            buttonTags.Command = CategoriesVM.ShowTagSelectionCommand;
            buttonTags.Label = "Tags";
            buttonTags.LargeImageSource = @"..\Images\Ribbon\TagLarge.png";
            buttonTags.ToolTipDescription = "Select Tags that should be automatically downloaded.";
            buttonTags.ToolTipTitle = "Tags";
            groupCategories.Items.Add(buttonTags);

            var buttonShows = new RibbonButtonVM();
            buttonShows.Command = CategoriesVM.ShowShowSelectionCommand;
            buttonShows.Label = "Shows";
            buttonShows.LargeImageSource = @"..\Images\Ribbon\ShowLarge.png";
            buttonShows.ToolTipDescription = "Select Shows that should be automatically downloaded.";
            buttonShows.ToolTipTitle = "Shows";
            groupCategories.Items.Add(buttonShows);

            var buttonSeries = new RibbonButtonVM();
            buttonSeries.Command = CategoriesVM.ShowSeriesSelectionCommand;
            buttonSeries.Label = "Series";
            buttonSeries.LargeImageSource = @"..\Images\Ribbon\SeriesLarge.png";
            buttonSeries.ToolTipDescription = "Select Series that should be automatically downloaded.";
            buttonSeries.ToolTipTitle = "Series";
            groupCategories.Items.Add(buttonSeries);

            categories.Groups.Add(groupCategories);

            // Set up group 'Update'
            var groupUpdate = new RibbonGroupVM();
            groupUpdate.Header = "Update";

            var buttonUpdate = new RibbonButtonVM();
            buttonUpdate.Command = CategoriesVM.UpdateCategoriesCommand;
            buttonUpdate.Label = "Update";
            buttonUpdate.LargeImageSource = @"..\Images\Ribbon\UpdateLarge.png";
            buttonUpdate.ToolTipDescription = "Update available tags from website.";
            buttonUpdate.ToolTipTitle = "Update";
            groupUpdate.Items.Add(buttonUpdate);

            categories.Groups.Add(groupUpdate);

            // Set up group 'Selection'
            var groupSelection = new RibbonGroupVM();
            groupSelection.Header = "Selection";

            var buttonSaveSelection = new RibbonButtonVM();
            buttonSaveSelection.Command = CategoriesVM.SaveSelectionCommand;
            buttonSaveSelection.Label = "Save";
            buttonSaveSelection.LargeImageSource = @"..\Images\Ribbon\SaveSelectionLarge.png";
            buttonSaveSelection.ToolTipDescription = "Save the category selection.";
            buttonSaveSelection.ToolTipTitle = "Save selection";
            groupSelection.Items.Add(buttonSaveSelection);

            categories.Groups.Add(groupSelection);

            // Set up group 'Filter'
            var groupFilter = new RibbonGroupVM();
            groupFilter.Header = "Filter";

            groupFilter.Items.Add(CategoriesVM.FilterRibbonTextBox);
            groupFilter.Items.Add(CategoriesVM.CaseSensitiveRibbonCheckBox);

            categories.Groups.Add(groupFilter);

            return categories;
        }

        /// <summary>
        /// Creates the dashboard tab.
        /// </summary>
        /// <returns>Returns the dashboard tab.</returns>
        private RibbonTabVM CreateDashboardTab()
        {
            var dashboard = new RibbonTabVM();
            dashboard.Header = RibbonTabName.DASHBOARD;

            var groupDisplay = new RibbonGroupVM();
            groupDisplay.Header = "Display";
            groupDisplay.Items.Add(DashboardVM.ShowSummaryRibbonToggleButton);

            var groupManagement = new RibbonGroupVM();
            groupManagement.Header = "Management";

            var buttonDownload = new RibbonButtonVM();
            buttonDownload.Command = DashboardVM.AddDownloadCommand;
            buttonDownload.Label = "Add download";
            buttonDownload.LargeImageSource = @"..\Images\Ribbon\AddDownloadLarge.png";
            buttonDownload.ToolTipDescription = "Adds the selected item to downloads.";
            buttonDownload.ToolTipTitle = "Add download";
            groupManagement.Items.Add(buttonDownload);

            dashboard.Groups.Add(groupDisplay);
            dashboard.Groups.Add(groupManagement);

            return dashboard;
        }

        /// <summary>
        /// Creates the download tab.
        /// </summary>
        /// <returns>Returns the download tab.</returns>
        private IRibbonTabVM CreateDownloadTab()
        {
            var downloads = new RibbonTabVM();
            downloads.Header = RibbonTabName.DOWNLOADS;

            // Set up group 'Control'
            var groupControl = new RibbonGroupVM();
            groupControl.Header = "Control";

            var startButton = new RibbonButtonVM();
            startButton.Command = DownloadsVM.StartDownloadsCommand;
            startButton.Label = "Start";
            startButton.LargeImageSource = @"..\Images\Ribbon\StartLarge.png";
            startButton.ToolTipDescription = "Starts downloading videos.";
            startButton.ToolTipTitle = "Start downloads";
            groupControl.Items.Add(startButton);

            var stopButton = new RibbonButtonVM();
            stopButton.Command = DownloadsVM.StopDownloadsCommand;
            stopButton.Label = "Stop";
            stopButton.LargeImageSource = @"..\Images\Ribbon\StopLarge.png";
            stopButton.ToolTipDescription = "Stops downloading videos.";
            stopButton.ToolTipTitle = "Stop downloads";
            groupControl.Items.Add(stopButton);

            var cleanButton = new RibbonButtonVM();
            cleanButton.Command = DownloadsVM.CleanDownloadsCommand;
            cleanButton.Label = "Clean";
            cleanButton.LargeImageSource = @"..\Images\Ribbon\CleanLarge.png";
            cleanButton.ToolTipDescription = "Remove finished and skipped downloads.";
            cleanButton.ToolTipTitle = "Clean downloads";
            groupControl.Items.Add(cleanButton);

            downloads.Groups.Add(groupControl);

            // Set up group 'Update'
            var groupUpdate = new RibbonGroupVM();
            groupUpdate.Header = "Update";

            var buttonUpdate = new RibbonButtonVM();
            buttonUpdate.Command = DownloadsVM.UpdateDownloadsCommand;
            buttonUpdate.Label = "Update";
            buttonUpdate.LargeImageSource = @"..\Images\Ribbon\RssUpdateLarge.png";
            buttonUpdate.ToolTipDescription = "Update available movies through RSS.";
            buttonUpdate.ToolTipTitle = "Update RSS";
            groupUpdate.Items.Add(buttonUpdate);

            downloads.Groups.Add(groupUpdate);

            return downloads;
        }

        #endregion Private Methods
    }
}