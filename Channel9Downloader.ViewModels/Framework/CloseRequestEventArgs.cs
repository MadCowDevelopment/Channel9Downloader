using System;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This is used to send result parameters to a CloseRequest
    /// </summary>
    public class CloseRequestEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CloseRequestEventArgs class.
        /// </summary>
        /// <param name="result">Result of the dialog.</param>
        internal CloseRequestEventArgs(bool? result)
        {
            Result = result;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the final result for ShowDialog.
        /// </summary>
        public bool? Result
        {
            get; private set;
        }

        #endregion Public Properties
    }
}