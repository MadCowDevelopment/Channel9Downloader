using System;
using System.ComponentModel.Composition;
using System.IO;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class contains helper functions for working with folders.
    /// </summary>
    [Export(typeof(IFolderUtils))]
    public class FolderUtils : IFolderUtils
    {
        #region Public Methods

        /// <summary>
        /// Gets the user's application data path.
        /// </summary>
        /// <returns>Returns the user's application data path.</returns>
        public string GetUserDataPath()
        {
            return @".\";

            // var dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // dir = Path.Combine(dir, "MadCowDevelopment", "Channel9Downloader");
            // if (!Directory.Exists(dir))
            // {
            //     Directory.CreateDirectory(dir);
            // }

            // return dir;
        }

        #endregion Public Methods
    }
}