using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RSSApp.Data.Repositories;
using RSSApp.Data.Repositories.Interfaces;
using RSSApp.Entity.Models;
using RSSApp.Service.Extentions;
using RSSApp.Service.Helpers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Xml.Linq;

namespace RSSApp.Service.Services
{
    public sealed class FeedService
    {
        private readonly IRepositoryBase<Feed> _repositoryBase;
        private static Dictionary<string, int> _FeedLinkDictionary = new();
        private readonly EntriesService _entriesService;
        private readonly HttpResponseMessage _httpResponseMessage;

        public FeedService()
        {
            _repositoryBase = new SQLiteRepository<Feed>();
            _entriesService = new EntriesService();
            _httpResponseMessage = new HttpResponseMessage();
        }


        public Feed Add(string link)
        {
            link.CheckForNull();
            return GetFeed(link);
        }
        private Feed GetFeed(string link)
        {
            if (_FeedLinkDictionary.ContainsKey(link.ToString()))
            {
                return LoadInclude().Where(feed => feed.Id == _FeedLinkDictionary[link]).FirstOrDefault();
            }
            else
                return SaveFeed(link);
        }
        private Feed SaveFeed(string link)
        {
            var xElm = XmlHelper.GetXElementByLink(link);
            var feed = CreateFeed(xElm);
            _repositoryBase.CreateAsync(feed);
            return feed;
        }
        private Feed CreateFeed(XElement xElm)
        {
            var feed = GetFeedWithoutEntriesFromXml(xElm);
            var entries = _entriesService.GetEntriesFromXml(xElm).Content as IEnumerable<Entries>;
            feed.Entries.AddRange(entries);
            return feed;
        }
        private Feed GetFeedWithoutEntriesFromXml(XElement xElm)
        {
            string title = xElm.Element("title").Value;
            string description = xElm.Element("description").Value;
            string link = xElm.Element("link").Value;

            return new Feed(title, description, link);
        }
        

        public async Task<IEnumerable<Feed>> LoadAsync()
        {
            return await _repositoryBase.LoadAsync();
        }

        public IEnumerable<Feed> LoadInclude()
        {
            return _repositoryBase.Include(feed => feed.Entries);
        }     
    }
}
