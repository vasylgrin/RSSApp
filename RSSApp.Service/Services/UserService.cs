using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RSSApp.Data.Repositories;
using RSSApp.Data.Repositories.Interfaces;
using RSSApp.Entity.Models;
using RSSApp.Service.Extentions;
using RSSApp.Service.Helpers;
using RSSApp.Service.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace RSSApp.Service.Services
{
    public sealed class UserService
    {
        private readonly IRepositoryBase<User> _repositoryBase;
        private readonly HttpResponseMessage _httpResponseMessage;
        private readonly EntriesService _entriesService;

        public UserService()
        {
            _repositoryBase = new SQLiteRepository<User>();
            _httpResponseMessage = new HttpResponseMessage();
            _entriesService = new EntriesService();
        }

        public async Task<HttpResponseMessage> AddAsync(string name, string password)
        {
            name.CheckForNull();
            password.CheckForNull();

            if (await IsExistNameAsync(name))
            {
                return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.Found, "This Username alredy exist.");
            }
            else
            {
                await _repositoryBase.CreateAsync(new User(name, password));
                return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.OK, "User successfully create.");
            }
        }
        private async Task<bool> IsExistNameAsync(string name)
        {
            var users = await LoadAsync();
            var isExistName = users.Any(user => user.Name == name);

            if (isExistName)
            {
                return true;
            }
            else
                return false;
        }


        public async Task<HttpResponseMessage> AuthenticationAsync(string name, string password)
        {
            name.CheckForNull();
            password.CheckForNull();

            if (await IsUserExistAsync(name, password))
            {
                return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.OK, CreateJwtToken(name, password));
            }
            else
            {
                return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.NotFound, "The user with the following parameters is not registered");
            }
        }
        private async Task<bool> IsUserExistAsync(string name, string password)
        {
            var users = await LoadAsync();
            var isExistUser = users.Any(user => user.Name == name && user.Password == password);
            
            if (isExistUser)
            {
                return true;
            }
            else
                return false;
        }
        private string CreateJwtToken(string name, string password)
        {
            var claimsList = new List<Claim>
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, name),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, password)
            };

            var signingCredentials = new SigningCredentials(SecretKeyHelper.GetSecretKey(), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                JwtSecurityConstant.Issure,
                JwtSecurityConstant.Audience,
                claimsList,
                DateTime.Now,
                DateTime.Now.AddMinutes(5),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<HttpResponseMessage> SetReadEntriesAsync(string name, string password, string entriesLink)
        {
            name.CheckForNull();
            password.CheckForNull();
            entriesLink.CheckForNull();

            var user = FindUser(name, password);
            if (IsEntriesExist(user, entriesLink))
            {
                return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.Found, "This entries has been added to your reading list");
            }

            var readEntries = await FindEntriesAsync(entriesLink);
            user.ReadingEntries.Add(new ReadEntries(readEntries));
            
            await _repositoryBase.UpdateAsync(user);
            return _httpResponseMessage.CreateResponseMessage(HttpStatusCode.OK, "Successfully deduce what you read");
        }
        private async Task<Entries> FindEntriesAsync(string link)
        {
            var allEntries = await _entriesService.LoadAsync();
            return allEntries.Where(entries => entries.Link == link).FirstOrDefault();
        }
        private User FindUser(string name, string password)
        {
            return LoadInclude().Where(user => user.Name == name && user.Password == password).FirstOrDefault();
        }
        private bool IsEntriesExist(User user, string entriesLink)
        {
            if (user.ReadingEntries.Any())
            {
                return user.ReadingEntries.Any(rEntries => rEntries.ReadEntry.Link == entriesLink);
            }
            return false;
        }


        public async Task<IEnumerable<User>> LoadAsync()
        {
            return await _repositoryBase.LoadAsync();
        }
        public IEnumerable<User> LoadInclude()
        {
            return _repositoryBase.Include(entries => entries.ReadingEntries);
        }
    }
}
