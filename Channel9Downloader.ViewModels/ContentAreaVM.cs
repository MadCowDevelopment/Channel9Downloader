using System.ComponentModel.Composition;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    /// <summary>
    /// This class manages the main content area.
    /// </summary>
    [Export(typeof(IContentAreaVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ContentAreaVM : ObservableObject, IContentAreaVM
    {
    }
}