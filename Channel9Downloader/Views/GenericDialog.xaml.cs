using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.Views
{
    /// <summary>
    /// Interaction logic for GenericDialog.xaml
    /// </summary>
    public partial class GenericDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDialog"/> class.
        /// </summary>
        public GenericDialog()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDialog"/> class.
        /// </summary>
        /// <param name="viewModel">The viewmodel that will be used as DataContext for this window.</param>
        public GenericDialog(ISimpleViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CloseRequest += ViewModelCloseRequest;
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Event handler for the close request.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void ViewModelCloseRequest(object sender, CloseRequestEventArgs e)
        {
            Close();
        }

        #endregion Private Methods
    }
}