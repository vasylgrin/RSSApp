using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace RSSApp.Service.Extentions.Tests
{
    [TestClass()]
    public class CreateHttpResponseMessageExtensionTests
    {
        [TestMethod()]
        public void CreateResponseMessage_StatusCode200AndMessage_StatusCode200()
        {
            // arrange

            var responseMessage = new HttpResponseMessage();
            var statusCode = HttpStatusCode.OK;
            string message = "OKEY";

            // act

            responseMessage = responseMessage.CreateResponseMessage(statusCode, message);

            // assert

            Assert.AreEqual(statusCode, responseMessage.StatusCode);
        }
    }
}