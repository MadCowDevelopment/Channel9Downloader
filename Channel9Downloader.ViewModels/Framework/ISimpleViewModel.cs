using System;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This interface is for viewmodels that support lifetime events.
    /// </summary>
    public interface ISimpleViewModel : IViewModelBase
    {
        #region Events

        /// <summary>
        /// This event should be raised to activate the UI.  Any view tied to this
        /// ViewModel should register a handler on this event and close itself when
        /// this event is raised.  If the view is not bound to the lifetime of the
        /// ViewModel then this event can be ignored.
        /// </summary>
        event EventHandler<EventArgs> ActivateRequest;

        /// <summary>
        /// This event should be raised to close the view.  Any view tied to this
        /// ViewModel should register a handler on this event and close itself when
        /// this event is raised.  If the view is not bound to the lifetime of the
        /// ViewModel then this event can be ignored.
        /// </summary>
        event EventHandler<CloseRequestEventArgs> CloseRequest;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the ActivatedCommand : Window Lifetime command
        /// </summary>
        RelayCommand ActivatedCommand
        {
            get;
        }

        /// <summary>
        /// Gets the CloseCommand : Window Lifetime command
        /// </summary>
        RelayCommand CloseCommand
        {
            get;
        }

        /// <summary>
        /// Gets the DeactivatedCommand : Window Lifetime command
        /// </summary>
        RelayCommand DeactivatedCommand
        {
            get;
        }

        /// <summary>
        /// Gets the LoadedCommand : Window/UserControl Lifetime command
        /// </summary>
        RelayCommand LoadedCommand
        {
            get;
        }

        /// <summary>
        /// Gets the UnloadedCommand : Window/UserControl Lifetime command
        /// </summary>
        RelayCommand UnloadedCommand
        {
            get;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// This raises the <see cref="SimpleViewModel.ActivateRequest"/> event to activate the UI.
        /// </summary>
        void RaiseActivateRequest();

        /// <summary>
        /// This raises the <see cref="SimpleViewModel.CloseRequest"/> event to close the UI.
        /// </summary>
        void RaiseCloseRequest();

        /// <summary>
        /// This raises the <see cref="SimpleViewModel.CloseRequest"/> event to close the UI.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        void RaiseCloseRequest(bool? dialogResult);

        #endregion Methods
    }
}