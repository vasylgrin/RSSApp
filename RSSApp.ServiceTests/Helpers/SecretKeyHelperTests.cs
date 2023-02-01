using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RSSApp.Service.Helpers.Tests
{
    [TestClass()]
    public class SecretKeyHelperTests
    {
        [TestMethod()]
        public void GetSecretKeyTest()
        {
            // arrange
            // act

            var key = SecretKeyHelper.GetSecretKey();

            // assert

            Assert.IsNotNull(key);
        }
    }
}