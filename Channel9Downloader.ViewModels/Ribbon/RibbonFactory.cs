using System.Collections.Generic;
using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Categories;

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
            result.Add(CreateCategoryTab());
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

            return categories;
        }

        #endregion Private Methods
    }
}