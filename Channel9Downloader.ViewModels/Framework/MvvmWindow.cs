using System.Windows;

namespace Channel9Downloader.ViewModels.Framework
{
    /// <summary>
    /// This class serves as base class for windows that can be closed from the viewmodel.
    /// </summary>
    public class MvvmWindow : Window, IViewService
    {
        #region Fields

        /// <summary>
        /// The ViewModel that serves as DataContext for this View.
        /// </summary>
        private SimpleViewModel _viewModel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MvvmWindow class.
        /// </summary>
        public MvvmWindow()
        {
            DataContextChanged += OnDataContextChanged;
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Event handler for the DataContextChanged event.
        /// Registers an event handler for the ViewModel's CloseRequest event.
        /// </summary>
        /// <param name="sender">Sender of the event (this class itself).</param>
        /// <param name="e">Event args of the event.</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = e.NewValue as SimpleViewModel;

            if (_viewModel != null)
            {
                _viewModel.CloseRequest += ViewModelCloseRequest;
            }
        }

        /// <summary>
        /// Event handler for the ViewModel's CloseRequest event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void ViewModelCloseRequest(object sender, CloseRequestEventArgs e)
        {
            DataContextChanged -= OnDataContextChanged;
            _viewModel.CloseRequest -= ViewModelCloseRequest;
            DialogResult = e.Result;
            Close();
        }

        #endregion Private Methods
    }
}