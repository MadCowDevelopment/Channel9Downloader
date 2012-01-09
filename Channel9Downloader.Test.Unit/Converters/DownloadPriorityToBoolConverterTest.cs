using System;
using System.Globalization;

using Channel9Downloader.Converters;
using Channel9Downloader.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel9Downloader.Test.Unit.Converters
{
    /// <summary>
    /// This class contains tests for the download priority converter.
    /// </summary>
    [TestClass]
    public class DownloadPriorityToBoolConverterTest
    {
        #region Fields

        /// <summary>
        /// The converter that will be tested.
        /// </summary>
        private DownloadPriorityToBoolConverter _converter;

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
        /// Verifies that the ConvertBack method returns correct values.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"..\..\..\Channel9Downloader.Test.Unit\Converters\TestData\DownloadPriorityToBoolConverter.xlsx")]
        [DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\DownloadPriorityToBoolConverter.xlsx", "ConvertBack$", DataAccessMethod.Sequential)]
        public void ConvertBackReturnsCorrectValues()
        {
            var value = (bool)TestContext.DataRow[0];
            DownloadPriority parameter;
            Enum.TryParse(TestContext.DataRow[1].ToString(), out parameter);
            DownloadPriority expected;
            if (Enum.TryParse(TestContext.DataRow[2].ToString(), out expected))
            {
                var actual = _converter.ConvertBack(value, typeof(bool), parameter, CultureInfo.InvariantCulture);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                var actual = _converter.ConvertBack(value, typeof(bool), parameter, CultureInfo.InvariantCulture);
                Assert.IsNull(actual);
            }
        }

        /// <summary>
        /// Verifies that the Convert method returns correct values.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"..\..\..\Channel9Downloader.Test.Unit\Converters\TestData\DownloadPriorityToBoolConverter.xlsx")]
        [DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\DownloadPriorityToBoolConverter.xlsx", "Convert$", DataAccessMethod.Sequential)]
        public void ConvertReturnsCorrectValues()
        {
            DownloadPriority value;
            Enum.TryParse(TestContext.DataRow[0].ToString(), out value);
            DownloadPriority parameter;
            Enum.TryParse(TestContext.DataRow[1].ToString(), out parameter);
            var expected = (bool)TestContext.DataRow[2];

            var actual = (bool)_converter.Convert(value, typeof(bool), parameter, CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, actual);
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
            _converter = new DownloadPriorityToBoolConverter();
        }

        #endregion Public Methods
    }
}