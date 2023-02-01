using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSSApp.Entity.Models;
using RSSApp.Service.Services;

namespace RSSApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedApiController : ControllerBase
    {
        private readonly FeedService _feedService;

        public FeedApiController()
        {
            _feedService = new FeedService();
        }

        [HttpGet, Route("all_feeds"), Authorize]
        public async Task<IEnumerable<Feed>> GetAllFeedsAsync()
        {
            return await _feedService.LoadAsync();
        }

        [HttpGet, Route("all_feeds/with_entries"), Authorize]
        public ObjectResult GetAllFeedsWithEntries()
        {
            return StatusCode(200, _feedService.LoadInclude());

            //return _feedService.LoadInclude();
        }
   
        [HttpPost, Route("add_feed")]
        public void AddFeed([FromBody] string link)
        {
            _feedService.Add(link);
        }
    }
}
