using System;
using System.ComponentModel.Composition;

namespace Channel9Downloader.Common
{
    /// <summary>
    /// This class contains methods and properties for date and time.
    /// </summary>
    [Export(typeof(IDate))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Date : IDate
    {
        #region Public Properties

        /// <summary>
        /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer,
        /// expressed as local time.
        /// </summary>
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        #endregion Public Properties
    }
}