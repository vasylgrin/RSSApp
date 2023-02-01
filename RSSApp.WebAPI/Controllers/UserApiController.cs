using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSSApp.Entity.Models;
using RSSApp.Service.Services;
using RSSApp.WebAPI.Models;

namespace RSSApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UserService _userService;
        public UserApiController()
        {
            _userService = new UserService();
        }

        [HttpPost, Route("registraition")]
        public async Task<HttpResponseMessage> RegistrationNewUserAsync([FromBody] Models.UserRequest user)
        {
            return await _userService.AddAsync(user.Name, user.Password);
        }


        [HttpPost, Route("Login")]
        public async Task<HttpResponseMessage> AuthenticateExistUser([FromBody] UserRequest user)
        {
            return await _userService.AuthenticationAsync(user.Name, user.Password);
        }


        [HttpPost, Route("set_read_entries"), Authorize]
        public async Task<HttpResponseMessage> Get([FromBody] UserRequest user, string entriesLink)
        {
            return await _userService.SetReadEntriesAsync(user.Name, user.Password, entriesLink);
        }

        [HttpPost, Route("get_all_read_entries"), Authorize]
        public IEnumerable<ReadEntries> GetAdllReadEntries([FromBody] UserRequest userModel)
        {
            return _userService.LoadInclude().Where(user=>user.Name == userModel.Name && user.Password == userModel.Password).FirstOrDefault().ReadingEntries;
        }
    }
}
