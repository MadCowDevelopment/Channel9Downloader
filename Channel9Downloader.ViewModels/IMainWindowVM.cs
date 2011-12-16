using System;
using System.Windows.Input;
using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This interface is for <see cref="MainWindowVM"/>.
    /// </summary>
    public interface IMainWindowVM : ISimpleViewModel
    {
        /// <summary>
        /// Gets a command that shows the settings view.
        /// </summary>
        ICommand ShowSettingsViewCommand { get; }

        void Initialize();

        event EventHandler<ShowDialogEventArgs> DialogRequested;
    }
}