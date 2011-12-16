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
        /// <summary>
        /// The DataContext for this view.
        /// </summary>
        private readonly IMainWindowVM _mainWindowVM;

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

        private static void MainWindowVMDialogRequested(object sender, ShowDialogEventArgs e)
        {
            var dialog = new GenericDialog(e.ViewModel);
            dialog.ShowDialog();
        }

        private void MainWindowVMCloseRequest(object sender, CloseRequestEventArgs e)
        {
            Close();
        }

        #endregion Constructors
    }
}