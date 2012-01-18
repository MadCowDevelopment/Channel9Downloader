using System;

using Channel9Downloader.Common;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class serves as base class for view models that require lifetime events.
    /// </summary>
    public class SimpleViewModel : BaseViewModel, ISimpleViewModel
    {
        #region Fields

        /// <summary>
        /// Command when the window is activated.
        /// </summary>
        private readonly RelayCommand _activatedCommand;

        /// <summary>
        /// Command when the window is closed.
        /// </summary>
        private readonly RelayCommand _closeCommand;

        /// <summary>
        /// Command when the window is deactivated.
        /// </summary>
        private readonly RelayCommand _deactivatedCommand;

        /// <summary>
        /// Command when the window is loaded.
        /// </summary>
        private readonly RelayCommand _loadedCommand;

        /// <summary>
        /// Command when the window is unloaded.
        /// </summary>
        private readonly RelayCommand _unloadedCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SimpleViewModel class.
        /// Wires up all the Window based Lifetime commands.
        /// </summary>
        protected SimpleViewModel()
        {
            _activatedCommand = new RelayCommand(p => OnWindowActivated());
            _deactivatedCommand = new RelayCommand(p => OnWindowDeactivated());
            _loadedCommand = new RelayCommand(p => OnWindowLoaded());
            _unloadedCommand = new RelayCommand(p => OnWindowUnloaded());
            _closeCommand = new RelayCommand(p => OnWindowClose());
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// This event should be raised to activate the UI.  Any view tied to this
        /// ViewModel should register a handler on this event and close itself when
        /// this event is raised.  If the view is not bound to the lifetime of the
        /// ViewModel then this event can be ignored.
        /// </summary>
        public event EventHandler<EventArgs> ActivateRequest;

        /// <summary>
        /// This event should be raised to close the view.  Any view tied to this
        /// ViewModel should register a handler on this event and close itself when
        /// this event is raised.  If the view is not bound to the lifetime of the
        /// ViewModel then this event can be ignored.
        /// </summary>
        public event EventHandler<CloseRequestEventArgs> CloseRequest;

        #endregion Events

        #region Public Properties

        /// <summary>
        /// Gets the ActivatedCommand : Window Lifetime command
        /// </summary>
        public RelayCommand ActivatedCommand
        {
            get { return _activatedCommand; }
        }

        /// <summary>
        /// Gets the CloseCommand : Window Lifetime command
        /// </summary>
        public RelayCommand CloseCommand
        {
            get { return _closeCommand; }
        }

        /// <summary>
        /// Gets the DeactivatedCommand : Window Lifetime command
        /// </summary>
        public RelayCommand DeactivatedCommand
        {
            get { return _deactivatedCommand; }
        }

        /// <summary>
        /// Gets the LoadedCommand : Window/UserControl Lifetime command
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get { return _loadedCommand; }
        }

        /// <summary>
        /// Gets the UnloadedCommand : Window/UserControl Lifetime command
        /// </summary>
        public RelayCommand UnloadedCommand
        {
            get { return _unloadedCommand; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// This raises the <see cref="ActivateRequest"/> event to activate the UI.
        /// </summary>
        public virtual void RaiseActivateRequest()
        {
            var handlers = ActivateRequest;

            // Invoke the event handlers
            if (handlers != null)
            {
                try
                {
                    handlers(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// This raises the <see cref="CloseRequest"/> event to close the UI.
        /// </summary>
        public virtual void RaiseCloseRequest()
        {
            var handlers = CloseRequest;

            // Invoke the event handlers
            if (handlers != null)
            {
                try
                {
                    handlers(this, new CloseRequestEventArgs(null));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// This raises the <see cref="CloseRequest"/> event to close the UI.
        /// </summary>
        /// <param name="dialogResult">The dialog result.</param>
        public virtual void RaiseCloseRequest(bool? dialogResult)
        {
            var handlers = CloseRequest;

            // Invoke the event handlers
            if (handlers != null)
            {
                try
                {
                    handlers(this, new CloseRequestEventArgs(dialogResult));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Allows Window.Activated hook
        /// </summary>
        protected virtual void OnWindowActivated()
        {
        }

        /// <summary>
        /// Allows Window.Close hook
        /// </summary>
        protected virtual void OnWindowClose()
        {
            RaiseCloseRequest();
        }

        /// <summary>
        /// Allows Window.Deactivated hook
        /// </summary>
        protected virtual void OnWindowDeactivated()
        {
        }

        /// <summary>
        /// Allows Window.Loaded/UserControl.Loaded hook
        /// </summary>
        protected virtual void OnWindowLoaded()
        {
        }

        /// <summary>
        /// Allows Window.Unloaded/UserControl.Unloaded hook
        /// </summary>
        protected virtual void OnWindowUnloaded()
        {
        }

        #endregion Protected Methods
    }
}