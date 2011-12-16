using System.ComponentModel.Composition;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels
{
    [Export(typeof(ISettingsVM))]
    public class SettingsVM : SimpleViewModel, ISettingsVM
    {
        public SettingsVM()
        {
            base.DisplayName = "Settings";
        }
    }
}
