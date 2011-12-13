using Channel9Downloader.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel9Downloader.Test.Integration
{
    /// <summary>
    /// Summary description for AvailableCategoriesTest
    /// </summary>
    [TestClass]
    public class AvailableCategoriesTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAllTags()
        {
            var browser = CreateChannel9CategoryBrowser();

            var tags = browser.GetAllTags();

            Assert.IsTrue(tags.Count > 0);
        }

        [TestMethod]
        public void GetAllShows()
        {
            var browser = CreateChannel9CategoryBrowser();

            var shows = browser.GetAllShows();

            Assert.IsTrue(shows.Count > 0);
        }

        [TestMethod]
        public void GetAllSeries()
        {
            var browser = CreateChannel9CategoryBrowser();

            var series = browser.GetAllSeries();

            Assert.IsTrue(series.Count > 0);
        }

        private Channel9CategoryBrowser CreateChannel9CategoryBrowser()
        {
            return new Channel9CategoryBrowser();
        }
    }
}
