﻿using System.Collections.Generic;
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
        #region Fields

        /// <summary>
        /// The categories viewmodel.
        /// </summary>
        private readonly ICategoriesVM _categoriesVM;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonFactory"/> class.
        /// </summary>
        /// <param name="categoriesVM">The categories viewmodel.</param>
        [ImportingConstructor]
        public RibbonFactory(ICategoriesVM categoriesVM)
        {
            _categoriesVM = categoriesVM;
        }

        #endregion Constructors

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

            var group = new RibbonGroupVM();
            group.Header = "Categories";

            var buttonTags = new RibbonButtonVM();
            buttonTags.Command = _categoriesVM.ShowTagSelectionCommand;
            buttonTags.Label = "Tags";
            buttonTags.LargeImageSource = @"..\Images\Ribbon\TagLarge.png";
            buttonTags.ToolTipDescription = "Select Tags that should be automatically downloaded.";
            buttonTags.ToolTipTitle = "Tags";
            group.Items.Add(buttonTags);

            var buttonShows = new RibbonButtonVM();
            buttonShows.Command = _categoriesVM.ShowShowSelectionCommand;
            buttonShows.Label = "Shows";
            buttonShows.LargeImageSource = @"..\Images\Ribbon\ShowLarge.png";
            buttonShows.ToolTipDescription = "Select Shows that should be automatically downloaded.";
            buttonShows.ToolTipTitle = "Shows";
            group.Items.Add(buttonShows);

            var buttonSeries = new RibbonButtonVM();
            buttonSeries.Command = _categoriesVM.ShowSeriesSelectionCommand;
            buttonSeries.Label = "Series";
            buttonSeries.LargeImageSource = @"..\Images\Ribbon\SeriesLarge.png";
            buttonSeries.ToolTipDescription = "Select Series that should be automatically downloaded.";
            buttonSeries.ToolTipTitle = "Series";
            group.Items.Add(buttonSeries);

            categories.Groups.Add(group);

            return categories;
        }

        #endregion Private Methods
    }
}