using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSSApp.Entity.Models;
using RSSApp.Service.Services;

namespace RSSApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesApiController : ControllerBase
    {
        private readonly EntriesService _entriesService;
        public EntriesApiController()
        {
            _entriesService = new EntriesService();
        }

        [HttpGet, Route("all_entries"), Authorize]
        public async Task<IEnumerable<Entries>> GetAllEntriesAsync()
        {
            return await _entriesService.LoadAsync();
        }

        [HttpGet, Route("AllEntreisFromStartDate"), Authorize]
        public async Task<IEnumerable<Entries>> GetEntriesFromStartDateAsync(string startDate)
        {
            return await _entriesService.GetEntriesFromStartDateAsync(startDate);
        }

        [HttpGet, Route("AllEntreisFromStartEndDate"), Authorize]
        public async Task<IEnumerable<Entries>> GetEntriesFromStartDateAsync(string startDate, string endDate)
        {
            return await _entriesService.GetEntriesFromStartEndDateAsync(startDate, endDate);
        }
    }
}
