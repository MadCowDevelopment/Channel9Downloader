using System;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Events
{
    public class ShowDialogEventArgs : EventArgs
    {
        public ISimpleViewModel ViewModel { get; private set; }

        public ShowDialogEventArgs(ISimpleViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
