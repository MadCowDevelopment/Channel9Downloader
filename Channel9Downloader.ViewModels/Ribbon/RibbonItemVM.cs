using Channel9Downloader.Entities;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class serves as base class for ribbon items.
    /// </summary>
    public abstract class RibbonItemVM : ObservableObject, IRibbonItemVM
    {
        public bool IsDropDownOpen { get; set; }
    }
}