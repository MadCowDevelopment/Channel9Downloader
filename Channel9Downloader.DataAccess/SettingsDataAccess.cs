using System.ComponentModel.Composition;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class is used for reading/writing the application settings.
    /// </summary>
    [Export(typeof(ISettingsDataAccess))]
    public class SettingsDataAccess : DataContractDataAccess<Settings>, ISettingsDataAccess
    {
        #region Public Methods

        /// <summary>
        /// Load settings from the specified file.
        /// </summary>
        /// <param name="filename">Name of the file that contains categories.</param>
        /// <returns>Returns the settings read from the file.</returns>
        public Settings LoadSettings(string filename)
        {
            return Load(filename);
        }

        /// <summary>
        /// Saves settings to the specified file.
        /// </summary>
        /// <param name="categories">Settings that should be saved.</param>
        /// <param name="filename">Name of the file that should be written.</param>
        public void SaveSettings(Settings categories, string filename)
        {
            Save(categories, filename);
        }

        #endregion Public Methods
    }
}