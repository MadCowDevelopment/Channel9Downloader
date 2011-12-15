using System.Collections.Generic;
using System.ComponentModel.Composition;
using Channel9Downloader.ViewModels.Categories;

namespace Channel9Downloader.ViewModels.Ribbon
{
    [Export(typeof(IRibbonFactory))]
    public class RibbonFactory : IRibbonFactory
    {
        private readonly ICategoriesVM _categoriesVM;

        [ImportingConstructor]
        public RibbonFactory(ICategoriesVM categoriesVM)
        {
            _categoriesVM = categoriesVM;
        }

        public List<IRibbonTabVM> CreateRibbonTabs()
        {
            var result = new List<IRibbonTabVM>();

            result.Add(CreateCategoryTab());

            return result;
        }

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


    }
}
