using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Channel9Downloader.Entities;

namespace Channel9Downloader.DataAccess
{
    public class Channel9CategoryBrowser
    {
        public List<Tag> GetAllTags()
        {
            return RetrieveTags();
        }

        public List<Show> GetAllShows()
        {
            var recurringCategories = RetrieveShowsOrSeries("http://channel9.msdn.com/Browse/Shows?sort=atoz&page={0}");
            return recurringCategories.Select(recurringCategory => new Show(recurringCategory)).ToList();
        }

        public List<Series> GetAllSeries()
        {
            var recurringCategories = RetrieveShowsOrSeries("http://channel9.msdn.com/Browse/Series?sort=atoz&page={0}");
            return recurringCategories.Select(recurringCategory => new Series(recurringCategory)).ToList();
        }

        private List<Tag> RetrieveTags()
        {
            var result = new List<Tag>();

            for (var c = (char)65; c <= 90; c++)
            {
                var links = GetTagsForCharacter(c);
                result.AddRange(links);
            }

            return result;
        }

        private List<RecurringCategory> RetrieveShowsOrSeries(string baseUrl)
        {
            var result = new List<RecurringCategory>();

            int i = 0;
            List<RecurringCategory> shows;
            while ((shows = GetShowsOrSeriesFromPage(baseUrl, i++)) != null && shows.Count > 0)
            {
                result.AddRange(shows);
            }

            return result;
        }

        private List<RecurringCategory> GetShowsOrSeriesFromPage(string baseUrl, int pageNumber)
        {
            var url = string.Format(baseUrl, pageNumber);
            var document = GetDocument(url);
            var tabContent = GetTabContent(document);

            var entries = from item in tabContent.Descendants("div")
                          where item.Attribute("class") != null && item.Attribute("class").Value == "entry-meta"
                          select item;

            return (from entry in entries
                    let title = entry.Element("a").Value
                    let relativePath = entry.Element("a").Attribute("href").Value
                    let description = entry.Elements("div").Where(p => p.Attribute("class").Value == "description").FirstOrDefault().Value.Trim()
                    select new RecurringCategory {Description = description, RelativePath = relativePath, Title = title}).ToList();
        }

        private IEnumerable<Tag> GetTagsForCharacter(char character)
        {
            var url = string.Format("http://channel9.msdn.com/Browse/Tags/FirstLetter/{0}#{0}", character);
            var document = GetDocument(url);
            var tabContent = GetTabContent(document);

            var lists = from item in tabContent.Descendants("ul")
                        where item.Attribute("class") != null && item.Attribute("class").Value == "tagList default"
                        select item.Descendants("a");

            if(lists.Count() < 1)
            {
                return new List<Tag>();
            }

            var list = lists.ElementAt(0);

            return from item in list select new Tag {RelativePath = item.Attribute("href").Value, Title = item.Value};
        }

        private XDocument GetDocument(string url)
        {
            var client = new WebClient();
            var data = client.DownloadString(url);
            data = data.Remove(0, 17);
            data = data.Replace("&", "");
            return XDocument.Parse(data);
        }

        private XElement GetTabContent(XDocument document)
        {
            var bodyElement = document.Element("html").Element("body");

            var tabContentDiv = from item in bodyElement.Descendants("div")
                                where item.Attribute("class") != null && item.Attribute("class").Value == "tab-content"
                                select item;

            return tabContentDiv.ElementAt(0);
        }
    }
}
