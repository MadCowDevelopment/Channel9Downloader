using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System;

using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    /// <summary>
    /// This class manages access to the global application settings.
    /// </summary>
    [Export(typeof(ISettingsManager))]
    public class SettingsManager : ISettingsManager
    {
        /// <summary>
        /// Folder utils for access to specific OS folders.
        /// </summary>
        private readonly IFolderUtils _folderUtils;

        /// <summary>
        /// The data access used for reading/writing settings to disc.
        /// </summary>
        private readonly ISettingsDataAccess _settingsDataAccess;

        /// <summary>
        /// Filename of the settings file.
        /// </summary>
        private readonly string _settingsFilename;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsManager"/> class.
        /// </summary>
        /// <param name="folderUtils">Folder utils for access to specific OS folders.</param>
        /// <param name="settingsDataAccess">The data access used for reading/writing settings to disc.</param>
        [ImportingConstructor]
        public SettingsManager(IFolderUtils folderUtils, ISettingsDataAccess settingsDataAccess)
        {
            _folderUtils = folderUtils;
            _settingsDataAccess = settingsDataAccess;
            _settingsFilename = Path.Combine(_folderUtils.GetUserDataPath(), "settings.xml");
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>Returns the settings.</returns>
        public Settings LoadSettings()
        {
            Settings settings = null;

            try
            {
                if (File.Exists(_settingsFilename))
                {
                    settings = _settingsDataAccess.LoadSettings(_settingsFilename);
                }
            }
            catch (SerializationException)
            {
                Console.WriteLine();
            }
            finally
            {
                if (settings == null)
                {
                    settings = new Settings();
                    SaveSettings(settings);
                }
            }

            return settings;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="settings">The settings to save.</param>
        public void SaveSettings(Settings settings)
        {
            try
            {
                _settingsDataAccess.SaveSettings(settings, _settingsFilename);
            }
            catch (SerializationException)
            {
                Console.WriteLine();
            }
        }
    }
}
