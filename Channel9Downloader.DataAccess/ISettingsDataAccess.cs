using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods to load/save settings.
    /// </summary>
    public interface ISettingsDataAccess
    {
        #region Methods

        /// <summary>
        /// Load settings from the specified file.
        /// </summary>
        /// <param name="filename">Name of the file that contains categories.</param>
        /// <returns>Returns the settings read from the file.</returns>
        Settings LoadSettings(string filename);

        /// <summary>
        /// Saves settings to the specified file.
        /// </summary>
        /// <param name="categories">Settings that should be saved.</param>
        /// <param name="filename">Name of the file that should be written.</param>
        void SaveSettings(Settings categories, string filename);

        #endregion Methods
    }
}