using System;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess.Events
{
    public class DownloadAddedEventArgs : EventArgs
    {
        public DownloadItem DownloadItem { get; set; }

        public DownloadAddedEventArgs(DownloadItem downloadItem)
        {
            DownloadItem = downloadItem;
        }
    }
}
