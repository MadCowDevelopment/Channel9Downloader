﻿using Channel9Downloader.DataAccess;
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
        /// Tests whether the series can be retrieved.
        /// </summary>
        [TestMethod]
        public void GetAllSeries()
        {
            var browser = CreateChannel9CategoryBrowser();

            var series = browser.GetAllCategories<Series>();

            Assert.IsTrue(series.Count > 0);
        }

        /// <summary>
        /// Tests whether the shows can be retrieved.
        /// </summary>
        [TestMethod]
        public void GetAllShows()
        {
            var browser = CreateChannel9CategoryBrowser();

            var shows = browser.GetAllCategories<Show>();

            Assert.IsTrue(shows.Count > 0);
        }

        /// <summary>
        /// Tests whether the tags can be retrieved.
        /// </summary>
        [TestMethod]
        public void GetAllTags()
        {
            var browser = CreateChannel9CategoryBrowser();

            var tags = browser.GetAllCategories<Tag>();

            Assert.IsTrue(tags.Count > 0);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates a category browser.
        /// </summary>
        /// <returns>Returns a category browser.</returns>
        private CategoryScraper CreateChannel9CategoryBrowser()
        {
            var webDownloader = new WebDownloader();
            return new CategoryScraper(webDownloader);
        }

        #endregion Private Methods
    }
}