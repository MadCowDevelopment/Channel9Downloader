using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class manages the downloads view.
    /// </summary>
    [Export(typeof(IDownloadsVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DownloadsVM : ViewModelBase, IDownloadsVM
    {
    }
}