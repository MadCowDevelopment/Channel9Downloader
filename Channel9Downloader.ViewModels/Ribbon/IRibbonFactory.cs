using System.Collections.Generic;

namespace Channel9Downloader.ViewModels.Ribbon
{
    public interface IRibbonFactory
    {
        List<IRibbonTabVM> CreateRibbonTabs();
    }
}