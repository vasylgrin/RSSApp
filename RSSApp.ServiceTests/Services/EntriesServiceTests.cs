using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSApp.Entity.Models;
using RSSApp.Service.Helpers;
using RSSApp.ServiceTests.Services;

namespace RSSApp.Service.Services.Tests
{
    [TestClass()]
    public class EntriesServiceTests : BaseServiceTest
    {
        private EntriesService _entriesService;
        private const string LINK = @"https://www.radiosvoboda.org/api/zrqiteuuir";


        [TestInitialize]
        public void Initialize()
        {
            _entriesService = new();
        }


        [TestMethod()]
        public void GetEntriesFromXml_RSSLink_Entries()
        {
            // arrange
            // act

            var xElm = XmlHelper.GetXElementByLink(LINK);
            var entries = _entriesService.GetEntriesFromXml(xElm);

            // assert

            Assert.IsNotNull(entries);
            Assert.AreNotEqual(0, entries.Count());
        }


        [TestMethod()]
        public async Task LoadAsyncTest()
        {
            // arrange     
            // act
            AddFeed();

            var entries = await _entriesService.LoadAsync();
            
            DeleteFeed();

            // assert

            Assert.IsNotNull(entries);
            Assert.AreNotEqual(0, entries.ToList().Count);
        }


        [TestMethod()]
        public async Task GetEntriesFromStartDateAsyncTest()
        {
            // arrange         
            string startDate = DateTime.Now.AddHours(-3).ToString();
            
            // act
            AddFeed();

            var entries = await _entriesService.GetEntriesFromStartDateAsync(startDate);

            DeleteFeed();
            // assert

            Assert.IsNotNull(entries);
            Assert.AreNotEqual(0, entries.ToList().Count);
        }

        [TestMethod()]
        public async Task GetEntriesFromStartEndDateAsyncTest()
        {
            // arrange
            
            string startDate = DateTime.Now.AddHours(-3).ToString();
            string endDate = DateTime.Now.AddHours(-1).ToString();

            // act
            AddFeed();

            var entries = await _entriesService.GetEntriesFromStartEndDateAsync(startDate, endDate);

            DeleteFeed();
            // assert

            Assert.IsNotNull(entries);
            Assert.AreNotEqual(0, entries.ToList().Count);
        }
    }
}