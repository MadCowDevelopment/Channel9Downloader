using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class manages the loading wait viewmodel.
    /// </summary>
    [Export(typeof(ILoadingWaitVM))]
    public class LoadingWaitVM : BaseViewModel, ILoadingWaitVM
    {
    }
}