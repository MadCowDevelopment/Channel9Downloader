using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is '<see langword="true"/>'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        /// <summary>
        /// Predicate that determines whether the action can be executed.
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Action that will be executed.
        /// </summary>
        private readonly Action<object> _execute;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// This event is fired when the can execute has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion Events

        #region Public Methods

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter">Parameter that influences execution.</param>
        /// <returns>Returns <see langword="true"/> if the command can execute, <see langword="false"/> otherwise.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Parameter for the command to execute.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion Public Methods
    }
}