using System;
using System.Collections.Generic;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    public class DownloadManagerInitializedEventArgs : EventArgs
    {
        public List<DownloadItem> DownloadItems { get; set; }

        public DownloadManagerInitializedEventArgs(List<DownloadItem> downloadItems)
        {
            DownloadItems = downloadItems;
        }
    }
}
