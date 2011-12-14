using System.Windows;

namespace Channel9Downloader.ViewModels.Framework
{
    #region Enumerations

    /// <summary>
    /// Available Button options. 
    /// Abstracted to allow some level of UI agnosticness.
    /// </summary>
    public enum CustomDialogButtons
    {
        /// <summary>
        /// Enumeration item that represents ok button only.
        /// </summary>
        Ok,

        /// <summary>
        /// Enumeration item that represents ok and cancel buttons.
        /// </summary>
        OkCancel,

        /// <summary>
        /// Enumeration item that represents yes and no buttons.
        /// </summary>
        YesNo,

        /// <summary>
        /// Enumeration item that represents yes, no and cancel buttons.
        /// </summary>
        YesNoCancel
    }

    /// <summary>
    /// Available Icon options.
    /// Abstracted to allow some level of UI Agnosticness
    /// </summary>
    public enum CustomDialogIcons
    {
        /// <summary>
        /// Enumeration item that represents no icon.
        /// </summary>
        None,

        /// <summary>
        /// Enumeration item that represent information icon.
        /// </summary>
        Information,

        /// <summary>
        /// Enumeration item that represents question mark icon.
        /// </summary>
        Question,

        /// <summary>
        /// Enumeration item that represents exclamation mark icon.
        /// </summary>
        Exclamation,

        /// <summary>
        /// Enumeration item that represents stop sign icon.
        /// </summary>
        Stop,

        /// <summary>
        /// Enumeration item that represents warning icon.
        /// </summary>
        Warning
    }

    /// <summary>
    /// Available DialogResults options.
    /// Abstracted to allow some level of UI Agnosticness
    /// </summary>
    public enum CustomDialogResults
    {
        /// <summary>
        /// Enumeration item that represent no result.
        /// </summary>
        None,

        /// <summary>
        /// Enumeration item that represents ok result.
        /// </summary>
        Ok,

        /// <summary>
        /// Enumeration item that represents cancel result.
        /// </summary>
        Cancel,

        /// <summary>
        /// Enumeration item that represents yes result.
        /// </summary>
        Yes,

        /// <summary>
        /// Enumeration item that represents no result.
        /// </summary>
        No
    }

    #endregion Enumerations

    /// <summary>
    /// This interface defines a interface that will allow 
    /// a ViewModel to show a <see cref="MessageBox"/>.
    /// </summary>
    public interface IMessageBoxService
    {
        #region Methods

        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="message">The error message</param>
        void ShowError(string message);

        /// <summary>
        /// Shows an information message
        /// </summary>
        /// <param name="message">The information message</param>
        void ShowInformation(string message);

        /// <summary>
        /// Displays a OK/Cancel dialog and returns the user input.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>User selection.</returns>
        CustomDialogResults ShowOkCancel(string message, CustomDialogIcons icon);

        /// <summary>
        /// Shows an warning message
        /// </summary>
        /// <param name="message">The warning message</param>
        void ShowWarning(string message);

        /// <summary>
        /// Displays a Yes/No dialog and returns the user input.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>User selection.</returns>
        CustomDialogResults ShowYesNo(string message, CustomDialogIcons icon);

        /// <summary>
        /// Displays a Yes/No/Cancel dialog and returns the user input.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>User selection.</returns>
        CustomDialogResults ShowYesNoCancel(string message, CustomDialogIcons icon);

        #endregion Methods
    }
}