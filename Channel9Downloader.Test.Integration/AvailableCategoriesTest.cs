using System.Collections.Generic;
using Channel9Downloader.DataAccess;
using Channel9Downloader.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel9Downloader.Test.Integration
{
    /// <summary>
    /// This class contains tests that are related to retrieving available categories.
    /// </summary>
    [TestClass]
    public class AvailableCategoriesTest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Tests whether the series can be retrieved at all.
        /// </summary>
        [TestMethod]
        public void GetAllSeriesReturnsAtLeastOneSeries()
        {
            var browser = CreateChannel9CategoryBrowser();

            var series = browser.GetAllCategories<Series>();

            Assert.IsTrue(series.Count > 0);
        }

        /// <summary>
        /// Tests whether the shows can be retrieved at all.
        /// </summary>
        [TestMethod]
        public void GetAllShowsReturnsAtLeastOneSeries()
        {
            var browser = CreateChannel9CategoryBrowser();

            var shows = browser.GetAllCategories<Show>();

            Assert.IsTrue(shows.Count > 0);
        }

        /// <summary>
        /// Tests whether the tags can be retrieved at all.
        /// </summary>
        [TestMethod]
        public void GetAllTagsReturnsAtLeastOneSeries()
        {
            var browser = CreateChannel9CategoryBrowser();

            var tags = browser.GetAllCategories<Tag>();

            Assert.IsTrue(tags.Count > 0);
        }

        /// <summary>
        /// Tests whether the retrieved series contain no duplicates.
        /// </summary>
        [TestMethod]
        public void NoDuplicateSeriesAreRetrieved()
        {
            var browser = CreateChannel9CategoryBrowser();

            var series = browser.GetAllCategories<Series>();

            Assert.IsTrue(AllElementsHaveDistinctRelativePaths(series));
        }

        /// <summary>
        /// Tests whether the retrieved shows contain no duplicates.
        /// </summary>
        [TestMethod]
        public void NoDuplicateShowsAreRetrieved()
        {
            var browser = CreateChannel9CategoryBrowser();

            var shows = browser.GetAllCategories<Show>();

            Assert.IsTrue(AllElementsHaveDistinctRelativePaths(shows));
        }

        /// <summary>
        /// Tests whether the retrieved tags contain no duplicates.
        /// </summary>
        [TestMethod]
        public void NoDuplicateTagsAreRetrieved()
        {
            var browser = CreateChannel9CategoryBrowser();

            var tags = browser.GetAllCategories<Tag>();

            Assert.IsTrue(AllElementsHaveDistinctRelativePaths(tags));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates a category browser.
        /// </summary>
        /// <returns>Returns a category browser.</returns>
        private static CategoryScraper CreateChannel9CategoryBrowser()
        {
            var webDownloader = new WebDownloader();
            return new CategoryScraper(webDownloader);
        }

        /// <summary>
        /// Checks whether all elements have distinct relative paths.
        /// </summary>
        /// <param name="categories">The categories to check.</param>
        /// <returns>Returns true if all elements have different relative paths, false otherwise.</returns>
        private static bool AllElementsHaveDistinctRelativePaths(IEnumerable<Category> categories)
        {
            foreach (var item in categories)
            {
                foreach (var compareItem in categories)
                {
                    if (compareItem == item)
                    {
                        continue;
                    }

                    if (compareItem.RelativePath == item.RelativePath)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion Private Methods
    }
}