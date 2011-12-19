using System.Windows.Input;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This interface is for <see cref="MainWindowVM"/>.
    /// </summary>
    public interface IMainWindowVM : ISimpleViewModel
    {
        #region Properties

        /// <summary>
        /// Gets a command that shows the settings view.
        /// </summary>
        ICommand ShowSettingsViewCommand
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes the viewmodel.
        /// </summary>
        void Initialize();

        #endregion Methods
    }
}