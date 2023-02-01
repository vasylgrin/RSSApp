using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSApp.Entity.Models;
using RSSApp.ServiceTests.Services;

namespace RSSApp.Service.Services.Tests
{
    [TestClass()]
    public class FeedServiceTests : BaseServiceTest
    {
        [TestMethod()]
       public void Add_FeedLink_NewFeed()
        {
            // arrange 
            // act

            var feed = AddFeed();
            
            DeleteFeed();
            
            // assert

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Entries);
            Assert.AreNotEqual(0, feed.Entries.Count);
        }


        [TestMethod()]
        public async Task LoadFeedAsyncTest()
        {
            // arrange

            var feedService = new FeedService();

            // act

            AddFeed();

            var feeds = await feedService.LoadAsync();

            DeleteFeed();
            
            // assert

            Assert.IsNotNull(feeds);
        }


        [TestMethod()]
        public void LoadFeedIncludeTest()
        {
            // arrange

            var feedService = new FeedService();

            // act

            AddFeed();

            var feeds = feedService.LoadInclude().ToList().FirstOrDefault();

            DeleteFeed();

            // assert

            Assert.IsNotNull(feeds);
            Assert.IsNotNull(feeds.Entries);
        }
    }
}