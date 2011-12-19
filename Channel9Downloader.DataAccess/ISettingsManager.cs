using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This interface provides methods for application settings management.
    /// </summary>
    public interface ISettingsManager
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>Returns the settings.</returns>
        Settings LoadSettings();

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="settings">The settings to save.</param>
        void SaveSettings(Settings settings);
    }
}