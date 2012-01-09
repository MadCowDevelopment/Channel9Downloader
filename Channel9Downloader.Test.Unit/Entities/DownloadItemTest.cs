using Channel9Downloader.Common;
using Channel9Downloader.Entities;
using Channel9Downloader.Test.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Channel9Downloader.Test.Unit.Entities
{
    /// <summary>
    /// Tests behavior of the <see cref="DownloadItem"/> class.
    /// </summary>
    [TestClass]
    public class DownloadItemTest
    {
        #region Fields

        /// <summary>
        /// The mock for date.
        /// </summary>
        private Mock<IDate> _date;

        /// <summary>
        /// The <see cref="DownloadItem"/> that will be tested.
        /// </summary>
        private DownloadItem _downloadItem;

        #endregion Fields

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
        /// Verifies that the default value for the priority is normal.
        /// </summary>
        [TestMethod]
        public void DefaultValueForPriorityIsNormal()
        {
            Assert.AreEqual(DownloadPriority.Normal, _downloadItem.Priority);
        }

        /// <summary>
        /// Cleans up after the tests in this class.
        /// </summary>
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        /// <summary>
        /// Initializes the tests in this class.
        /// </summary>
        [TestInitialize]
        public void MyTestInitialize()
        {
            _date = new Mock<IDate>();
            _downloadItem = new DownloadItem(_date.Object);
        }

        /// <summary>
        /// Verifies that setting the priority to a different value raises the PropertyChanged event.
        /// </summary>
        [TestMethod]
        public void SettingPriorityToDifferentValueRaisesPropertyChanged()
        {
            Assertion.PropertyChangedIsCalled(_downloadItem, DownloadItem.PROP_PRIORITY, DownloadPriority.High);
        }

        /// <summary>
        /// Verifies that setting the priority to the same value doesn't rais the PropertyChanged event.
        /// </summary>
        [TestMethod]
        public void SettingPriorityToSameValueTwiceDoesntRaisePropertyChanged()
        {
            Assertion.PropertyChangedIsNotCalled(_downloadItem, DownloadItem.PROP_PRIORITY, DownloadPriority.Normal);
        }

        #endregion Public Methods
    }
}