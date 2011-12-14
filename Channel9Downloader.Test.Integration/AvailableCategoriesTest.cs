using Channel9Downloader.DataAccess;

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

            var series = browser.GetAllSeries();

            Assert.IsTrue(series.Count > 0);
        }

        /// <summary>
        /// Tests whether the shows can be retrieved.
        /// </summary>
        [TestMethod]
        public void GetAllShows()
        {
            var browser = CreateChannel9CategoryBrowser();

            var shows = browser.GetAllShows();

            Assert.IsTrue(shows.Count > 0);
        }

        /// <summary>
        /// Tests whether the tags can be retrieved.
        /// </summary>
        [TestMethod]
        public void GetAllTags()
        {
            var browser = CreateChannel9CategoryBrowser();

            var tags = browser.GetAllTags();

            Assert.IsTrue(tags.Count > 0);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates a category browser.
        /// </summary>
        /// <returns>Returns a category browser.</returns>
        private Channel9CategoryBrowser CreateChannel9CategoryBrowser()
        {
            var webDownloader = new WebDownloader();
            return new Channel9CategoryBrowser(webDownloader);
        }

        #endregion Private Methods
    }
}