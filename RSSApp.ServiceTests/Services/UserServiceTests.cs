using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSApp.Data.Repositories;
using RSSApp.Entity.Models;
using RSSApp.ServiceTests.Services;
using System.Net;

namespace RSSApp.Service.Services.Tests
{
    [TestClass()]
    public class UserServiceTests : BaseServiceTest
    {
        private const string _NAME = "name";
        private const string _PASSWORD = "password";
        private UserService _userService;


        [TestInitialize]
        public void Innitilasize()
        {
            _userService = new UserService();
        }


        [TestMethod()]
        public async Task AddAsync_NameAndPassword_NewRegisteredUser()
        {
            // arrange           
            // act

            var responseMessage = await _userService.AddAsync(_NAME, _PASSWORD);

            DeleteUser();
            
            // assert

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [TestMethod()]
        public async Task AddAsync_ExistName_StatusCode302()
        {
            // arrange
            // act

            await _userService.AddAsync(_NAME, _PASSWORD);
            var responseMessage = await _userService.AddAsync(_NAME, _PASSWORD);

            DeleteUser();

            // assert

            Assert.AreEqual(HttpStatusCode.Found, responseMessage.StatusCode);
        }


        [TestMethod()]
        public async Task AuthenticationAsync_NameAndPassword_StatusCode200()
        {
            // arrange
            // act
            
            await _userService.AddAsync(_NAME, _PASSWORD);
            var responseMessage = await _userService.AuthenticationAsync(_NAME, _PASSWORD);

            DeleteUser();

            // assert

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [TestMethod()]
        public async Task AuthenticationAsync_NameAndPassword_StatusCode404()
        {
            // arrange

            string name = "ssss";
            string password = "dddd";

            // act

            var responseMessage = await _userService.AuthenticationAsync(name, password);

            // assert

            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }


        [TestMethod()]
        public async Task SetReadEntriesAsync_NamePaswordAndEntryLink_StatusCode200()
        {
            // arrange
            // act

            var feed = await SetDatatForSetReadEntriesAync();
            var responseMessage = await _userService.SetReadEntriesAsync(_NAME, _PASSWORD, feed.Entries.First().Link);
            CheckIsUserExist(feed);
            
            // assert

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [TestMethod()]
        public async Task SetReadEntriesAsync_TheSameNamePaswordAndEntryLink_StatusCode302()
        {
            // arrange
            // act
            var feed = await SetDatatForSetReadEntriesAync();       
            
            await _userService.SetReadEntriesAsync(_NAME, _PASSWORD, feed.Entries.First().Link);
            var responseMessage = await _userService.SetReadEntriesAsync(_NAME, _PASSWORD, feed.Entries.First().Link);
            
            CheckIsUserExist(feed);
            // assert

            Assert.AreEqual(HttpStatusCode.Found, responseMessage.StatusCode);
        }
        
        private async Task<Feed> SetDatatForSetReadEntriesAync()
        {
            var feed = AddFeed();
            await _userService.AddAsync(_NAME, _PASSWORD);

            return feed;
        }
        private void CheckIsUserExist(Feed feed)
        {
            var user = _userService.LoadInclude().Where(user => user.Name == _NAME && user.Password == _PASSWORD).FirstOrDefault();
            var isExistEntryLink = user.ReadingEntries.Any(entry => entry.ReadEntry.Link == feed.Entries.First().Link);

            DeleteFeed();
            DeleteUser();

            Assert.IsNotNull(user);
            Assert.IsTrue(isExistEntryLink);
        }


        [TestMethod()]
        public async Task LoadIncludeTest()
        {
            // arrange
            // act
            
            await _userService.AddAsync(_NAME, _PASSWORD);
            var feed = AddFeed();
            await _userService.SetReadEntriesAsync(_NAME, _PASSWORD, feed.Entries.First().Link);
            
            var user = _userService.LoadInclude().First();

            DeleteUser();
            DeleteFeed();
            // assert

            Assert.IsNotNull(user);

            Assert.IsNotNull(user);
            Assert.AreNotEqual(0, user.ReadingEntries.Count);
        }

        [TestMethod()]
        public async Task LoadAsyncTest()
        {
            // arrange
            // act
           
            await _userService.AddAsync(_NAME, _PASSWORD);

            var users = await _userService.LoadAsync();

            DeleteUser();
            
            // assert

            Assert.IsNotNull(users);
            Assert.AreNotEqual(0, users.ToList().Count);
        }


        private async void DeleteUser()
        {
            var users = await new SQLiteRepository<User>().LoadAsync();
            await new SQLiteRepository<User>().DeleteRangeAsync(users);
        }
    }
}