using RSSApp.Data.Repositories;
using RSSApp.Data.Repositories.Interfaces;
using RSSApp.Entity.Models;
using RSSApp.Service.Extentions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Xml.Linq;

namespace RSSApp.Service.Services
{
    public class EntriesService
    {
        private readonly IRepositoryBase<Entries> _repositoryBase;
        private readonly HttpResponseMessage _httpResponseMessage;

        public EntriesService()
        {
            _repositoryBase = new SQLiteRepository<Entries>();
            _httpResponseMessage = new HttpResponseMessage();
        }


        public IEnumerable<Entries> GetEntriesFromXml(XElement xElement)
        {
            var xElements = xElement.Descendants("item");

            return CreateEntries(xElements);
        }
        private IEnumerable<Entries> CreateEntries(IEnumerable<XElement> xElements)
        {
            var EntriesList = new List<Entries>();

            foreach (var xElm in xElements)
            {
                string title = xElm.Element("title").Value;
                string description = xElm.Element("description").Value;
                string link = xElm.Element("link").Value;

                string pubDat = xElm.Element("pubDate").Value;
                DateTime dateTime = pubDat.ParseToDateTime();

                EntriesList.Add(new Entries(title, description, link, dateTime));
            }

            return EntriesList;
        }


        public async Task<IEnumerable<Entries>> GetEntriesFromStartDateAsync(string startDate)
        {
            var entries = await LoadAsync();
            return entries.Where(entries => entries.TimeOfPublication >= startDate.ParseToDateTime());
        }
        public async Task<IEnumerable<Entries>> GetEntriesFromStartEndDateAsync(string startDate, string endDate)
        {
            var entries = await GetEntriesFromStartDateAsync(startDate);
            return entries.Where(entries => entries.TimeOfPublication <= endDate.ParseToDateTime());
        }


        public Task<IEnumerable<Entries>> LoadAsync()
        {
            return _repositoryBase.LoadAsync();
        }
    }
}
