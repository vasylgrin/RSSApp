using RSSApp.Data.Repositories;
using RSSApp.Entity.Models;
using RSSApp.Service.Services;

namespace RSSApp.ServiceTests.Services
{
    public class BaseServiceTest
    {
        protected Feed _FeedWhichNeedtoDelete;
        protected Feed AddFeed()
        {
            const string LINK = "https://www.radiosvoboda.org/api/zrqiteuuir";
            _FeedWhichNeedtoDelete = new FeedService().Add(LINK);
            return _FeedWhichNeedtoDelete;
        }

        protected async void DeleteFeed()
        {
            await new SQLiteRepository<Feed>().DeleteAsync(_FeedWhichNeedtoDelete);
        }
    }
}
