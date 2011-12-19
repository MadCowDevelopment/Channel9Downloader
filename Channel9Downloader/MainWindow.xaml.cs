using System;

using Channel9Downloader.Composition;
using Channel9Downloader.ViewModels;
using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;
using Channel9Downloader.Views;

namespace Channel9Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Fields

        /// <summary>
        /// The DataContext for this view.
        /// </summary>
        private readonly IMainWindowVM _mainWindowVM;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var composer = new DependencyComposer();
                composer.ComposeExportedValue<IDependencyComposer>(composer);
                _mainWindowVM = composer.GetExportedValue<IMainWindowVM>();
                DataContext = _mainWindowVM;

                _mainWindowVM.Initialize();
                _mainWindowVM.CloseRequest += MainWindowVMCloseRequest;
                _mainWindowVM.DialogRequested += MainWindowVMDialogRequested;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion Constructors

        #region Private Static Methods

        /// <summary>
        /// Event handler for the dialog requested event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private static void MainWindowVMDialogRequested(object sender, DialogRequestEventArgs e)
        {
            var dialog = new GenericDialog(e.ViewModel);
            dialog.ShowDialog();
        }

        #endregion Private Static Methods

        #region Private Methods

        /// <summary>
        /// Event handler for the close request.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void MainWindowVMCloseRequest(object sender, CloseRequestEventArgs e)
        {
            Close();
        }

        #endregion Private Methods
    }
}