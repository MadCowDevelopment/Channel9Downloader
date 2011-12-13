using System;
using Channel9Downloader.DataAccess;

namespace Channel9Downloader.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var browser = new Channel9CategoryBrowser();
            var tags = browser.GetAllTags();
            var shows = browser.GetAllShows();
            var series = browser.GetAllSeries();

            Console.WriteLine("END");
        }


    }
}
