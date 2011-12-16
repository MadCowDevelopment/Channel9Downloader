using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.Views
{
    /// <summary>
    /// Interaction logic for GenericDialog.xaml
    /// </summary>
    public partial class GenericDialog
    {
        public GenericDialog()
        {
        }

        public GenericDialog(ISimpleViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CloseRequest += ViewModelCloseRequest;
        }

        private void ViewModelCloseRequest(object sender, CloseRequestEventArgs e)
        {
            Close();
        }
    }
}
