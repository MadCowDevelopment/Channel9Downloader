using System;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    public class DownloadRemovedEventArgs : EventArgs
    {
        public DownloadItem DownloadItem { get; set; }

        public DownloadRemovedEventArgs(DownloadItem downloadItem)
        {
            DownloadItem = downloadItem;
        }
    }
}
